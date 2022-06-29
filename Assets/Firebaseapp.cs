using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using Firebase;
using Unity.UI;
using System.Text.RegularExpressions;
using Firebase.Auth;
using Firebase.Analytics;
using UnityEngine.UI;
using TMPro;
using Firebase.Extensions;
using System;
using Firebase.Storage;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine.Networking;

public class SignupUser
{
    
    public string username;
    public string email;
    public string password;
    public string ID;

    public SignupUser()
    {
    }

    public SignupUser(string username, string email ,string password ,string ID)
    {
        this.username = username;
        this.email = email;
        this.password = password;
        this.ID = ID;
    }
}

public class FileUplaod
{

    public string path;
    public string name;

    public FileUplaod()
    {
    }

    public FileUplaod(string path ,string name)
    {
        this.path = path;
        this.name = name;
    }
}


public class Firebaseapp : MonoBehaviour
{

    List<string> urls = new List<string>();
    [SerializeField]
    AnimationClip animfile;
    [SerializeField]
    GameObject SignupEmail ,SignupPassword ,Signupname ,rePassword;
    [SerializeField]
    GameObject LoginEmail, LoginPassword;
    private DependencyStatus dependencyStatus;

    public static string ID;


    


    public virtual void Start()
    {
        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                Awake();
            }
            else
            {
                Debug.LogError(
                "Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void Awake()
    {
        Debug.Log("hello");
    }

    public void signup()
    {
        string email = SignupEmail.GetComponent<TMP_InputField>().text;
        string password = SignupPassword.GetComponent<TMP_InputField>().text;
        string password2 = rePassword.GetComponent<TMP_InputField>().text;
        string name = Signupname.GetComponent<TMP_InputField>().text;
        Debug.Log(email);
        
        Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        Regex emailregex = new Regex("^\\S+@\\S+\\.\\S+$");
        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasMinimum8Chars = new Regex(@".{8,}");
        if (name != ""&&emailregex.IsMatch(email) && hasUpperChar.IsMatch(password) && hasNumber.IsMatch(password) && hasMinimum8Chars.IsMatch(password)&&password==password2)
        {
            auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                // Firebase user has been created.
                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);

                DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
                
                SignupUser user = new SignupUser(name, email ,password ,newUser.UserId);
                string json = JsonUtility.ToJson(user);

                reference.Child("users").Child(newUser.UserId).SetRawJsonValueAsync(json);
                Debug.Log(json);

                
              



            });

        }

    }

    // Start is called before the first frame update
    public void Login()
    {
        string email = LoginEmail.GetComponent<TMP_InputField>().text; ;
        string password = LoginPassword.GetComponent<TMP_InputField>().text;

        Regex emailregex = new Regex("^\\S+@\\S+\\.\\S+$");
        
        if (emailregex.IsMatch(email))
        {
            Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
                ID = newUser.UserId;
            });
        }
        

        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        //string id = reference.Push().Key;

        
        //reference.Child("users").Child("info").Child(id).SetValueAsync(name);
        //Debug.Log(id);
        

    }

    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;
    // Update is called once per frame
    void Update()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                ID = user.UserId;
                string fileurl = "https://firebasestorage.googleapis.com/v0/b/ar-motion-capture.appspot.com/o/animations%2Fanimation4.anim?alt=media&token=f2319144-4f52-42fe-a107-b7361e77328d";
                //StartCoroutine(GetFile(fileurl, "anima"));
                downloadFile();
                
                Debug.Log("Signed in " + user.UserId);
            }
        }

    }
    IEnumerator GetFile(string url, string file_name)
    {

        Debug.Log("fwqfwefwef");
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string savePath = string.Format("{0}/{1}.anim", Application.persistentDataPath, file_name);
                Debug.Log(savePath);
                System.IO.File.WriteAllText(savePath, www.downloadHandler.text);
            }
        }
    }



    public void downloadFile()
    {
        
      

        //string fileurl = "https://firebasestorage.googleapis.com/v0/b/ar-motion-capture.appspot.com/o/animations%2Fanimation4.anim?alt=media&token=f2319144-4f52-42fe-a107-b7361e77328d";
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        FirebaseDatabase.DefaultInstance
      .GetReference("users/"+ID+"/AnimationList")
      .GetValueAsync().ContinueWithOnMainThread(task => {
          if (task.IsFaulted)
          {
              Debug.Log(task.Exception);
              // Handle the error...
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;
              
              foreach(DataSnapshot snap in snapshot.Children)
              {
                  
                  //get all urls for user animations
                  urls.Add(snap.Child("path").Value.ToString());
                  Debug.Log(urls[0]);
                  //GetFile(urls[0] ,"FileName");

              }
          }
      });

        

    }



    


    public void UploadFile()
    {

        FirebaseStorage storage = FirebaseStorage.DefaultInstance;

        string path = "gs://ar-motion-capture.appspot.com/animations/";
        string fileName = "animation4.anim";
        // Create a storage reference from our storage service
        StorageReference storageRef =
        storage.GetReferenceFromUrl(path+fileName);
        

        storageRef.PutFileAsync("E:/GP/moCap_AR/Assets/Animation4.anim").ContinueWith((task) => {
            if (task.IsFaulted || task.IsCanceled)
            {
                Debug.Log(task.Exception.ToString());
                // Uh-oh, an error occurred!
            }
            else
            {
                // Metadata contains file metadata such as size, content-type, and md5hash.
                StorageMetadata metadata = task.Result;
                string md5Hash = metadata.Md5Hash;

                storageRef.GetDownloadUrlAsync().ContinueWithOnMainThread(task => {
                    if (!task.IsFaulted && !task.IsCanceled)
                    {
                        Debug.Log("Download URL: " + task.Result);

                        FileUplaod user = new FileUplaod(task.Result.ToString(), fileName);
                        string json = JsonUtility.ToJson(user);
                        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
                        string id = reference.Push().Key;
                        reference.Child("users").Child(ID).Child("AnimationList").Child(id).SetRawJsonValueAsync(json);
                    }
                });

                
              
            }
        });




    }
}
