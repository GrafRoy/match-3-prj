using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;
using Firebase.Database;

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
            Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
            return;
            }
            else if (task.IsFaulted) {
            Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
            return;
            }
            else
            {
                newUser = task.Result;
            }

            // Firebase user has been created.
            string json = JsonUtility.ToJson(newUser);
            var mDatabaseRef = DBreference.Child("users").Child(email).SetRawJsonValueAsync(json);
            Debug.LogFormat("This is working", newUser.DisplayName, newUser.UserId);
    
            
            
        });
    }

    public void OnSignInButtonClicked() => auth.SignInWithEmailAndPasswordAsync(email = UsernameInputField.text, password = PasswordInputField.text).ContinueWith(task =>
    {
        if (task.IsCanceled)
        {
            Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
            return;
        }
        else if (task.IsFaulted)
        {
            Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
            return;
        }
        else
        {
            newUser = task.Result;
            Panel.SetActive(true);
        }
        newUser = task.Result;

    });

    public void PopUpWindow()
    {
        if(Panel.activeSelf == false)
        {
            Debug.Log("Shit ain't workin");
            Panel.SetActive(true);
        }
        else if(Panel.activeSelf == true)
        {
            Debug.Log("Why this shit up");
            Panel.SetActive(false);
        }
    }
}
