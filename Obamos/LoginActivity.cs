using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using Firebase.Auth;

using Java.Interop;
using System.Net.Http;
using Firebase.Iid;
using Android.Gms.Tasks;

namespace Obamos
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity, IOnCompleteListener
    {
        private TextInputEditText usernameTextbox;
        private TextInputEditText password;
        private FirebaseAuth firebaseAuth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login);
            // Create your application here
            usernameTextbox = FindViewById<TextInputEditText>(Resource.Id.txtbox_username);
            password = FindViewById<TextInputEditText>(Resource.Id.txtbox_password);
            firebaseAuth = FirebaseAuth.Instance;

        }

        [Export("onClickLoginButton")]
        public async void onClickLoginButton(View v)
        {
            try
            {
                var user = await firebaseAuth.SignInWithEmailAndPasswordAsync(usernameTextbox.Text, password.Text);
                if (user == null)
                {
                    Toast.MakeText(this, "Giriş yapılamadı", ToastLength.Short).Show();
                }
                else
                {
                    var token = await user.User.GetIdTokenAsync(true);
      
                    Toast.MakeText(this, "Giriş yapıldı... Hoş Geldiniz", ToastLength.Long).Show();
                    this.Finish();

                    var instId = FirebaseInstanceId.Instance.GetInstanceId().AddOnCompleteListener(this, this);


              



                }
            }
            catch (System.Exception ex)
            {
                Toast.MakeText(this, "Giriş yapılamadı: " + ex.Message, ToastLength.Short).Show();

            }

           
        }

         async public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {

                var result_ = task.Result.Class.GetMethod("getToken").Invoke(task.Result).ToString();
                HttpClient cl = new HttpClient();

                await cl.GetAsync("https://obamos.000webhostapp.com/api/add_device.php?mail=" + usernameTextbox.Text + "&token=" + result_);
            }
        }
    }
}