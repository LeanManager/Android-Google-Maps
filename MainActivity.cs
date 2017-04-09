using Android.App;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace DroidMapping
{
    [Activity(Label = "DroidMapping", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IOnMapReadyCallback
    {
		GoogleMap map;

        protected override void OnCreate(Bundle bundle)
        {
			//var mf = FragmentManager.FindFragmentById(Resource.Id.map) as MapFragment;

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

			// var mf = FragmentManager.FindFragmentById(Resource.Id.map) as MapFragment;

			var mapFragment = MapFragment.NewInstance();

			FragmentTransaction trans = FragmentManager.BeginTransaction();

			trans.Add(Android.Resource.Id.Content, mapFragment);
			trans.Commit();

			mapFragment.GetMapAsync(this);
        }

		static readonly LatLng Location_Xamarin = new LatLng(37.80, -122.40);
		static readonly LatLng Location_NewYork = new LatLng(40.77, -73.98);
		static readonly LatLng Location_Chicago = new LatLng(41.88, -87.63);
		static readonly LatLng Location_Dallas = new LatLng(32.90, -97.03);

		public void OnMapReady(GoogleMap googleMap)
		{
			map = googleMap;
			map.MyLocationEnabled = true;
			map.UiSettings.CompassEnabled = true;
			map.UiSettings.MyLocationButtonEnabled = true;
			map.UiSettings.ZoomControlsEnabled = true; 

			map.AddMarker(new MarkerOptions()
			              .SetPosition(Location_NewYork)
			              .SetTitle("Manhattan")
			              .SetSnippet("Zwaby HQ"));

			map.AddMarker(new MarkerOptions()
	           .SetPosition(Location_Xamarin)
	           .SetTitle("Xamarin HQ")
	           .SetSnippet("Where the magic happens")
	           .SetIcon(BitmapDescriptorFactory
			   .DefaultMarker(BitmapDescriptorFactory.HueGreen)));

			Marker chicagoMarker = map.AddMarker(new MarkerOptions()
	                                  .SetPosition(Location_Chicago)
	                                  .SetTitle("Chicago")
	                                  .Draggable(true)
	                                  .SetIcon(BitmapDescriptorFactory
		                              .DefaultMarker(BitmapDescriptorFactory.HueYellow)));

			Marker dallasMarker = map.AddMarker(new MarkerOptions()
	                                 .SetPosition(Location_Dallas)
	                                 .SetTitle("Dallas")
	                                 .SetSnippet("Go Cowboys!")
	                                 .InfoWindowAnchor(1, 0)
	                                 .SetIcon(BitmapDescriptorFactory
		                             .DefaultMarker(BitmapDescriptorFactory.HueBlue)));

			// assign handler as anonymous function
			map.MarkerClick += delegate (object sender, GoogleMap.MarkerClickEventArgs e)
			{
				if (e.Marker.Equals(dallasMarker))
				{
					dallasMarker.Flat = !dallasMarker.Flat;
					dallasMarker.ShowInfoWindow();
				}
				else
				{
			    	// Execute default behavior for other markers.
                    // Could also just execute the following line for every
                    // marker..
                    e.Handled = false;      
				}
			};

			map.InfoWindowClick += (sender, e) =>
            {
	           if (e.Marker.Id == chicagoMarker.Id)
	           {
		           e.Marker.SetIcon(BitmapDescriptorFactory
			       .DefaultMarker(BitmapDescriptorFactory.HueRose));
	           }
            };

			map.MapClick += (sender, e) =>
            {
	           if (!chicagoMarker.IsInfoWindowShown)
	           {
		           chicagoMarker.SetIcon(BitmapDescriptorFactory
			       .DefaultMarker(BitmapDescriptorFactory.HueYellow));
	           }
            };
		}
    }
}

