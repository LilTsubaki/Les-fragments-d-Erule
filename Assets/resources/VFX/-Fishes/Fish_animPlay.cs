//using UnityEngine;
//using System.Collections;

//public class CharacterTrigger : MonoBehaviour
//{

//    private bool bellowsAnim = false;

//    void Update()
//    {
//        for i 

//        if (bellowsAnim == true)
//        {
//            gameObject.GetComponent<Animation>().Play();

//            if (0.95f < Gauge_component.texturesWithMaterial[Gauge_index].material.GetFloat("_Value"))
//            {
//                tempScore += 100;
//                newScore = tempScore.ToString("#     #     #     #");
//                Score_component.texts[Score_index].text = newScore;
//            }
//        }

//        if (doorOpen)
//        {
//            oldRot = rot;
//            rot += rotSpeed;
//            if (rot > maxRot) rot = oldRot;
//        }
//        else
//        {
//            oldRot = rot;
//            rot -= rotSpeed;
//            if (rot < 0) rot = oldRot;
//        }

//        door.transform.eulerAngles = new Vector3(0.0f, rot, 0.0f);

//        Gauge_component.texturesWithMaterial[Gauge_index].material.SetFloat("_Value", gaugeLife);

//    }


//    void OnTriggerEnter(Collider other)
//    {

//        if (other.gameObject.tag == "Bellows_Trigger")
//        {
//            Textures_component.textures[XIcon_index].enabled = true;

//            bellowsAnim = true;
//        }


//        if (other.gameObject.tag == "Door")
//        {
//            Textures_component.textures[DoorIcon_index].enabled = true;

//            doorOpen = true;

//            tempScore += 100;
//            newScore = tempScore.ToString("#     #     #     #");
//            Score_component.texts[Score_index].text = newScore;
//        }

//    }


//    void OnTriggerExit(Collider other)
//    {

//        Textures_component.textures[XIcon_index].enabled = false;
//        Textures_component.textures[DoorIcon_index].enabled = false;


//        bellowsAnim = false;


//        doorOpen = false;

//    }


////}
////using UnityEngine;
////using System.Collections;

////public class TexturePan : MonoBehaviour
////{

////    public float speedU = -0.5F;
////    public float speedV = -1.0F;
////    /*public float panSpeed = 3.0f;
	
////	private bool panBoost = false; */

////    void Update()
////    {

////        /*if (panBoost==true && (Input.GetButtonDown ("Use")))
////		{
////			speedU *= panSpeed;
////			speedV *= panSpeed;	
////		}
////		*/
////        Vector2 offset = Mathf.Repeat(Time.time, 4.0f) * new Vector2(speedU, speedV);
////        GetComponent<Renderer>().material.mainTextureOffset = offset;

////    }
