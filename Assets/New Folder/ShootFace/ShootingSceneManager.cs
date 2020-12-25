using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using Windows.Kinect;
using Windows.Kinect;
using System.IO;
using System;

public class ShootingSceneManager : MonoBehaviour
{
    // Serialize Fields
    public GUITexture backgroundImage = null;
    public GUITexture faceImage = null;
    //public float smoothFactor = 5f;
    public HandRaiseGestureListener handRaiseGestureListener = null;
    [SerializeField]
    int coverBoxPadding = 100;
    [SerializeField]
    UnityEngine.Color boxColor = UnityEngine.Color.black;
    [SerializeField]
    float faceRatioFactor = 500;
    public List<GameObject> list;

    // joints index
    private int headJointIndex;
    private int shoulderLeftIndex;
    private int shoulderRightIndex;
    private int footLeftIndex;

    Texture2D backgroundTexture2D;

    private long primaryUserId;
    private int userCount;

    KinectManager manager;
    public string namePhoto="";
    public Vector3 positionHead;
    public bool photo=true;
    public GUITexture ava;
    void Start()
    {
        headJointIndex = (int)KinectInterop.JointType.Head;
        shoulderLeftIndex = (int)KinectInterop.JointType.ShoulderLeft;
        shoulderRightIndex = (int)KinectInterop.JointType.ShoulderRight;
        footLeftIndex = (int)KinectInterop.JointType.FootLeft;
    }

    void Update()
    {
        manager = KinectManager.Instance;
        if (manager && manager.IsInitialized())
        {
            if (backgroundImage && (backgroundImage.texture == null))
            {
                backgroundTexture2D = manager.GetUsersClrTex();
                backgroundImage.texture = backgroundTexture2D;
            }

            if (manager.IsUserDetected())
            {
                userCount = manager.GetUsersCount();

                for (int i = 0; i < userCount; i++)
                {
                    long userId = manager.GetUserIdByIndex(i);
                    Vector2 headPos = getColorPosOfJoint(manager, userId, headJointIndex);
                    Vector2 shoulderLeftPos = getColorPosOfJoint(manager, userId, shoulderLeftIndex);
                    Vector2 shoulderRightPos = getColorPosOfJoint(manager, userId, shoulderRightIndex);
                    Vector2 footLeftPos = getColorPosOfJoint(manager, userId, footLeftIndex);

                    DrawRectangle(backgroundTexture2D,
                        new Vector2(shoulderLeftPos.x - coverBoxPadding, headPos.y - coverBoxPadding),
                        new Vector2(shoulderRightPos.x + coverBoxPadding, footLeftPos.y + coverBoxPadding),
                        boxColor);
                    backgroundTexture2D.Apply();

                }

                 if (handRaiseGestureListener.IsHandRaise())
                {
                    Debug.Log("Hand raise detected");
                    GameObject.Find("SoundController").GetComponent<SoundController>().playSoundKinect();
                    // Capture user photo
                    manager.SetPrimaryUserID(handRaiseGestureListener.GetHandRaiseUserId());
                    long primaryUserId = manager.GetPrimaryUserID();
                    positionHead = manager.GetJointPosition(primaryUserId, headJointIndex);
                  //  Debug.Log("Bandau"+positionHead.y);
                    Vector2 pUserHeadPos = getColorPosOfJoint(manager, primaryUserId, headJointIndex);
                    float distanceToCam = manager.GetUserBodyData(primaryUserId).position.z;

                    // size ảnh to hơn khi người chơi đứng gần kinect hơn. e.g: size ảnh = factor/distance to kinect => 150 = 300/2;
                    int faceTextureSize = (int)faceRatioFactor / (int)distanceToCam;
                    //Debug.Log("size : " + faceTextureSize);
                    Texture2D faceImageTexture = new Texture2D(faceTextureSize, faceTextureSize);
                    if (photo && faceImage!=null)
                    {
                        cropImage(manager.GetUsersClrTex(), faceImageTexture, pUserHeadPos, faceTextureSize);
                        faceImage.texture = faceImageTexture;
                     //   faceImage.texture = ava.texture;
                        // Save texture to image
						GameData.Instance().myPathImage = saveTextureToImage(faceImageTexture);
                        photo = false;
                    }
                    list[0].SetActive(true);
                    list[1].SetActive(false);
                  //  hub.enabled = true;
                }
            }
        }
    }

    Vector2 getColorPosOfJoint(KinectManager manager, long userId, int jointIndex)
    {
        if (manager.IsJointTracked(userId, jointIndex))
        {
            Vector3 posJoint = manager.GetJointKinectPosition(userId, jointIndex);

            if (posJoint != Vector3.zero)
            {
                // 3d position to depth
                Vector2 posDepth = manager.MapSpacePointToDepthCoords(posJoint);
                ushort depthValue = manager.GetDepthForPixel((int)posDepth.x, (int)posDepth.y);

                if (depthValue > 0)
                {
                    // depth pos to color pos
                    Vector2 posColor = manager.MapDepthPointToColorCoords(posDepth, depthValue);

                    return posColor;
                }
            }
        }
        return Vector2.zero;
    }

    void DrawRectangle(Texture2D texture, Vector2 topLeft, Vector2 bottomRight, UnityEngine.Color color)
    {
        // TODO: Validate point and create suitable rect


        // Draw lines
        KinectInterop.DrawLine(texture, Mathf.Clamp((int)topLeft.x, 0, texture.width - 1), Mathf.Clamp((int)topLeft.y, 0, texture.height - 1),
                                        Mathf.Clamp((int)bottomRight.x, 0, texture.width - 1), Mathf.Clamp((int)topLeft.y, 0, texture.height - 1), color);
        KinectInterop.DrawLine(texture, Mathf.Clamp((int)topLeft.x, 0, texture.width - 1), Mathf.Clamp((int)topLeft.y, 0, texture.height - 1),
                                        Mathf.Clamp((int)topLeft.x, 0, texture.width - 1), Mathf.Clamp((int)bottomRight.y, 0, texture.height - 1), color);
        KinectInterop.DrawLine(texture, Mathf.Clamp((int)bottomRight.x, 0, texture.width - 1), Mathf.Clamp((int)bottomRight.y, 0, texture.height - 1),
                                        Mathf.Clamp((int)bottomRight.x, 0, texture.width - 1), Mathf.Clamp((int)topLeft.y, 0, texture.height - 1), color);
        KinectInterop.DrawLine(texture, Mathf.Clamp((int)bottomRight.x, 0, texture.width - 1), Mathf.Clamp((int)bottomRight.y, 0, texture.height - 1),
                                        Mathf.Clamp((int)topLeft.x, 0, texture.width - 1), Mathf.Clamp((int)bottomRight.y, 0, texture.height - 1), color);
    }

    void cropImage(Texture2D baseTexture, Texture2D destinationTexture, Vector2 centerPoint, int size)
    {
        //  Debug.Log("height : " + baseTexture.height + " width :" + baseTexture.width);
        Debug.Log("centerPoint = " + centerPoint.ToString());
        int xPos = ((int)centerPoint.x - size / 2) > 0 ? (int)centerPoint.x - size / 2 : 0;
        Debug.Log("xpos = " + xPos);
        int yPos = ((int)centerPoint.y - size / 2) > 0 ? (int)centerPoint.y - size / 2 : 0;
        Debug.Log("ypos = " + yPos);

        //lấy pixels data , mảng color
        Color[] color = baseTexture.GetPixels(xPos, yPos, size, size);

        // Flip the texture upside-down
        Array.Reverse(color, 0, color.Length);

        destinationTexture.SetPixels(color);
        destinationTexture.Apply();
    }

    public string saveTextureToImage(Texture2D texture)
    {
        string fileName = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/img/players/" + fileName, bytes);
        namePhoto = fileName;
        return fileName;        
    }

    private void OnDisable()
    {
        Destroy(backgroundTexture2D);
    }
}
