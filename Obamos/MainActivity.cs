using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Auth;
using Java.Interop;
using System;

namespace Obamos
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button loginBtn;
        private Button registerBtn;
        private RelativeLayout foreignUserView;
        private RelativeLayout loggedUserView;
        private FirebaseApp firebaseApp;
        private FirebaseAuth authIntance;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            loginBtn = this.FindViewById<Button>(Obamos.Resource.Id.button1);
            registerBtn = this.FindViewById<Button>(Obamos.Resource.Id.button2);
            loginBtn.Click += new EventHandler((v,e) =>
             {
                 ShowActivity(typeof(LoginActivity));
             });
            registerBtn.Click += new EventHandler((v, e) =>
            {
                ShowActivity(typeof(RegisterActivity));
            });
            foreignUserView = FindViewById<RelativeLayout>(Resource.Id.ForeignUserView);
            loggedUserView = FindViewById<RelativeLayout>(Resource.Id.LoggedUserView);
        }
        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);
            setupInitialize();
        }
        private void ShowActivity(Type type)
        {
            Intent niyet = new Intent(this, type);
            StartActivity(niyet);
        }
        private void setupInitialize()
        {


            if (foreignUserView != null && loggedUserView != null)
            {

                firebaseApp = FirebaseApp.InitializeApp(this);
                authIntance = FirebaseAuth.GetInstance(firebaseApp);

                regenerateUi();
            }
        }

        private void regenerateUi()
        {

            var user = authIntance.CurrentUser;
            if (user != null)
            {

                foreignUserView.Visibility = ViewStates.Invisible;
                loggedUserView.Visibility = ViewStates.Visible;
            }
            else
            {
                loggedUserView.Visibility = ViewStates.Invisible;
                foreignUserView.Visibility = ViewStates.Visible;
            }
        }

        [Export("logoutClicked")]
        public void logoutClicked (View v)
        {
            authIntance.SignOut();
            regenerateUi();
        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);
            regenerateUi();
        }
    }
}

