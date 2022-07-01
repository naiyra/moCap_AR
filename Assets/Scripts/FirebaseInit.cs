using Firebase;
using Firebase.Analytics;
using Firebase.Auth;
using UnityEngine;

public class FirebaseInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {

            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            

        });
       

    }


}
