using System;

using Android.App;

#if RELEASE
[assembly: MetaDataAttribute("com.google.android.maps.API_KEY", Value = "release key goes here")]
#else
[assembly: MetaDataAttribute("com.google.android.maps.API_KEY", Value = "AIzaSyApXjRraKhZWlU-BRmW9XAvuQGwpC-VlW4")]
#endif
