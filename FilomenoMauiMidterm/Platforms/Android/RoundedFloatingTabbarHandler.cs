using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Views;
using Google.Android.Material.BottomNavigation;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;
using static Android.Views.ViewGroup;

namespace FilomenoMauiMidterm
{
    internal class RoundedFloatingTabbarHandler: ShellRenderer
    {
        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {
            return new RoundedFloatingBottomNavViewAppearanceTracker(this, shellItem);
        }

    }

    internal class RoundedFloatingBottomNavViewAppearanceTracker : ShellBottomNavViewAppearanceTracker
    {
        public RoundedFloatingBottomNavViewAppearanceTracker(IShellContext shellContext, ShellItem shellItem) : base(shellContext, shellItem)
        {
                
        }

        public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            base.SetAppearance(bottomView, appearance);
            var tabbarDrawable = new GradientDrawable();
            //Corner radius is by pair (x and y) that's why we have 8 values
            //tabbarDrawable.SetCornerRadii([40, 40, 40, 40, 0, 0, 0, 0]);
            
            //If we want just all corners to be 50           
            tabbarDrawable.SetCornerRadius(0);

            tabbarDrawable.SetColor(appearance.EffectiveTabBarBackgroundColor.ToPlatform());

            bottomView.SetBackground(tabbarDrawable);
            
            ViewGroup.LayoutParams layoutParameters = bottomView.LayoutParameters;
            if (layoutParameters is MarginLayoutParams marginLayoutParams)
            {
                marginLayoutParams.SetMargins(0, 0, 0, 0);
                bottomView.LayoutParameters = marginLayoutParams;
            }

        }
    }

}
