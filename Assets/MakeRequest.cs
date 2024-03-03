using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using TMPro;
using System;
using System.Threading;
using System.Linq;

public class MakeRequest : MonoBehaviour
{
    

    private List<ChatMessage> messages = new List<ChatMessage>();
    public GameObject speak;
    public GameObject avatar;
    public GameObject bubble;
    public TextMeshProUGUI bubbletext;

    /*
     * Makes the request to chatGPT, grabs ChatGPT's reponse, and plays it (if necessary)
     * 
     * @param: None
     * 
     * @return: None
     * 
     */
    public async void Request(string s)
    {


        var openai = new OpenAIApi();
        try
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = s
            };
            if (messages.Count == 0) newMessage.Content = $"In a brief statement, can you please explain the term {s}";

            messages.Add(newMessage);
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                MaxTokens = 128,
                Model = "gpt-3.5-turbo-0613",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                
                messages.Add(message);


                //avatar.GetComponent<Appear>().appear();
                //speak.GetComponent<UIManager>().SpeechPlayback(message.Content.ToString());
                bubbletext.text = $"ChatGPT says:\n{message.Content}";
                bubble.SetActive(true);
            }






        }
        catch (Exception ex)
        {
            Debug.LogError("An error occurred during the API request: " + ex.Message);
        }
    }


}
