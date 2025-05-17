using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace FilomenoMauiMidterm.Utils
{
    public class TappedAnimationBehavior : Behavior<View>
    {
        public static readonly BindableProperty ScaleDownToProperty =
            BindableProperty.Create(nameof(ScaleDownTo), typeof(double), typeof(TappedAnimationBehavior), 0.95);

        public static readonly BindableProperty AnimationDurationProperty =
            BindableProperty.Create(nameof(AnimationDuration), typeof(uint), typeof(TappedAnimationBehavior), (uint)80); // Note: uint here

        public double ScaleDownTo
        {
            get => (double)GetValue(ScaleDownToProperty);
            set => SetValue(ScaleDownToProperty, value);
        }

        public uint AnimationDuration // Changed to uint
        {
            get => (uint)GetValue(AnimationDurationProperty);
            set => SetValue(AnimationDurationProperty, value);
        }

        protected override void OnAttachedTo(View bindable)
        {
            bindable.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => await AnimateView(bindable))
            });
            base.OnAttachedTo(bindable);
        }

        private async Task AnimateView(View view)
        {
            await Task.WhenAll(
                view.ScaleTo(ScaleDownTo, AnimationDuration, Easing.SpringOut), // Now accepts uint
                view.FadeTo(0.8, (uint)(AnimationDuration / 2) // Cast to uint
           
                ));

            await Task.WhenAll(
                view.ScaleTo(1, AnimationDuration, Easing.SpringIn),
                view.FadeTo(1, AnimationDuration)
            );
        }
    }
}
