using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System;

namespace Obamos
{
    [Activity(Label = "Register Application", Theme = "@style/AppTheme")]
    public class RegisterActivity : AppCompatActivity
    {


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.register);

           
            
        }

     
    }
}

