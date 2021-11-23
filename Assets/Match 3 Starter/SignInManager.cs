using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using Firebase.Database;
using System.Diagnostics;

public class SignInManager : MonoBehaviour
{
    public InputField UsernameInputField;
    public Text UsernameText;
    public InputField PasswordInputField;
    public Text PasswordText;
    public string email;
    public string password;
    public GameObject Panel;
    public FirebaseAuth auth;
    public Firebase.Auth.FirebaseUser newUser; 
    public DatabaseReference DBreference;
    public FirebaseDatabase database = FirebaseDatabase.GetInstance("https://match3tiles-cf880-default-rtdb.firebaseio.com/");

    // Start is called before the first frame update

    void Start()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnSignUpButtonClicked()
    {
        auth.CreateUserWithEmailAndPasswordAsync(email = UsernameInputField.text, password = PasswordInputField.text).ContinueWith(task => {
            if (task.IsCanceled) {
            return;
            }
            else if (task.IsFaulted) {
            return;
            }    
        });


        DatabaseNewWrite();
        PopUpWindow();

    }

    public void OnSignInButtonClicked()
    { 
        auth.SignInWithEmailAndPasswordAsync(email = UsernameInputField.text, password = PasswordInputField.text).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                return;
            }
            else if (task.IsFaulted)
            {
            return;
            } 

    });

        //DatabaseWrite();
        PopUpWindow();
    }

    public void DatabaseNewWrite()
    {
        //string json = JsonUtility.ToJson(email);
        //DBreference.Child("User").Child(UsernameInputField.text).SetRawJsonValueAsync(json).ContinueWith(task => {
        //if (task.IsCanceled)
        //{
        //  UnityEngine.Debug.Log("successfully added data to firebase");
        //}
        //else
        //{
        //  UnityEngine.Debug.Log("not successful");
        //}
        //});
        DBreference.Push().Child("User").Child(email).SetValueAsync(email);
    }


    public void PopUpWindow()
    {
        if(Panel.activeSelf == false)
        {
        
            Panel.SetActive(true);
        }
        else if(Panel.activeSelf == true)
        {
            
            Panel.SetActive(false);
        }
    }
}
