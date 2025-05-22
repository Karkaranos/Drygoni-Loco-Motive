using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace NewDialogue
{
    public class DialogueSaveBehavior : MonoBehaviour
    {
        [SerializeField]private int[] viewedConversations;
        private int[] availableConversations;
        private int[] unreachableConversations;
        private int currentConversation;
        public string[] temp;

        private string dialogueSaveDataPath;
        // Start is called before the first frame update
        void Start()
        {
            dialogueSaveDataPath = Application.streamingAssetsPath + "/DialogueInfo.txt";

            temp = File.ReadAllLines(dialogueSaveDataPath)[0].Split(';');

            currentConversation = StringToInt(temp[0].Substring(21));
            viewedConversations = StringToInt(temp[1].Split(',', ':'));
            unreachableConversations = StringToInt(temp[2].Split(',', ':'));
            availableConversations = StringToInt(temp[3].Split(',', ':'));


        }

        /// <summary>
        /// Takes a string value of presumably all integers and converts it to an integer value
        /// </summary>
        /// <param name="s">The string to convert</param>
        /// <returns>The value as an int</returns>
        int StringToInt(string s)
        {
            int asInt = 0;
            int iterations = 0;
            while(s.Length > iterations)
            {
                if(asInt > 0)
                {
                    asInt *= 10;
                }
                switch(s.Substring(iterations,1))
                {
                    case "1":
                        asInt += 1;
                        break;
                    case "2":
                        asInt += 2;
                        break;
                    case "3":
                        asInt += 3;
                        break;
                    case "4":
                        asInt += 4;
                        break;
                    case "5":
                        asInt += 5;
                        break;
                    case "6":
                        asInt += 6;
                        break;
                    case "7":
                        asInt += 7;
                        break;
                    case "8":
                        asInt += 8;
                        break;
                    case "9":
                        asInt += 9;
                        break;
                    default:
                        break;
                }
                iterations++;
            }
            return asInt;
        }

        /// <summary>
        /// Converts an array of strings into an array of ints
        /// Calls StringToInt(string)
        /// </summary>
        /// <param name="s">The array of strings to convert</param>
        /// <returns>The array as integers</returns>
        int[] StringToInt(string[] s)
        {
            int[] result = new int[s.Length-1];
            int resultIndex = 0;
            for(int i=1; i<s.Length; i++, resultIndex++)
            {
                result[resultIndex] = StringToInt(s[i]);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        string IntToString(int i)
        {
            string result = "";
            while(i > -1)
            {
                if(i==0)
                {
                    result += "0";
                    i = -1;
                }

                if(i>99 && i<1000)
                {
                    result += i / 100;
                    if(i%100 < 9)
                    {
                        result += "0";
                    }
                    i = i % 100;

                }

                if(i<100 && i>9)
                {
                    result += i / 10;
                    i = i % 10;
                }

                if(i<10)
                {
                    result += i;
                    break;
                }
            }
            return result;
        }

        //TODO: get all messages in this conversation and return them
        public DialogueMessage[] StartConversation(int newConversation)
        {
            currentConversation = newConversation;
            temp[0] = temp[0].Substring(0, 20) + IntToString(newConversation);

            //TODO: Remove current conversation from available conversations

            //TODO: Move any now unavailable paths to unavailable conversations

            //Update the file with the new information
            File.WriteAllText(dialogueSaveDataPath, temp[0] + ";");
            for (int i=1; i<4; i++)
            {
                File.AppendAllText(dialogueSaveDataPath, temp[i] + ";");
            }
        }
    }
}