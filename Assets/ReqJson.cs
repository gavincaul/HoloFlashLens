using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using System;
using System.Text.RegularExpressions;

public class ReqJson : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI errorText;
    public JSONParse jsonParser;
    public SetManager setManager;
    public TextMeshProUGUI placeholder;
    public GameObject KeyboardButton;
    public TextMeshProUGUI keyboardtxt;

    private bool isUsernameChecked = false;
    private bool isPasswordChecked = false;


    private string usernameCHK;
    private string passwordCHK;


    public void RequestUserData()
    {
        StartCoroutine(LoginRequest("gavin", "gavinpass"));
        errorText.text = "";
        if (isUsernameChecked)
        {
            isUsernameChecked = true;
            usernameCHK = usernameInput.text;
            usernameInput.text = "";
            placeholder.text = "Enter password";
        }
        else if (isPasswordChecked)
        {
            passwordCHK = usernameInput.text;
            placeholder.text = "Enter User ID";
            StartCoroutine(LoginRequest(usernameCHK, passwordCHK));
        }
    }



    private IEnumerator LoginRequest(string username, string password)
    {
        User user = new User
        {
            username = username,
            password = password
        };

        string json = Newtonsoft.Json.JsonConvert.SerializeObject(user);
        Debug.Log(json);

        using (UnityWebRequest request = new UnityWebRequest("http://34.42.246.209:5000/login_unity", "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            DownloadHandlerBuffer dH = new DownloadHandlerBuffer();
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = dH;

            yield return request.SendWebRequest();


            if (request.result != UnityWebRequest.Result.Success)
            {
                errorText.text = $"Error: {request.error}";
            }
            else
            {
                if (request.responseCode == 200)
                {
                    string id = request.downloadHandler.text;
                    Debug.Log($"ID: {id}");
                    StartCoroutine(FetchUserSets(id.Replace("\"", ""), user));
                    
                }
                else
                {
                    errorText.text = "Login Failed";
                }
            }
        }
    }
    private IEnumerator FetchUserSets(string userId, User u)
    {
        userId = Regex.Replace(userId, @"\t|\n|\r", "");
        string setsUrl = $"http://34.42.246.209:5000/user/{userId}/sets";
        Debug.Log(setsUrl);

        using (UnityWebRequest setsRequest = UnityWebRequest.Get(setsUrl))
        {
            string auth = u.username + ":" + u.password;
            Debug.Log(auth);
            string authHeader = "Basic " + System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(auth));
            setsRequest.SetRequestHeader("AUTHORIZATION", authHeader);
            Debug.Log("where we at");
            yield return setsRequest.SendWebRequest();

            if (setsRequest.result != UnityWebRequest.Result.Success)
            {
                errorText.text = "Error fetching user sets: " + setsRequest.error;
            }
            else
            {
                string setsResponse = setsRequest.downloadHandler.text;
                Debug.Log(setsResponse);
                KeyboardButton.SetActive(false);
                keyboardtxt.gameObject.SetActive(false);
                jsonParser.ParseJSON(setsResponse);
                
            }
        }
    }
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
    }


}
