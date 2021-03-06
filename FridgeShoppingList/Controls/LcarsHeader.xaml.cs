﻿using Microsoft.Toolkit.Uwp.UI.Animations;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace FridgeShoppingList.Controls
{
    [ContentProperty(Name = nameof(HeaderContent))]
    public sealed partial class LcarsHeader : UserControl
    { 
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(LcarsHeader), new PropertyMetadata(string.Empty, (d, e) =>
            {
                LcarsHeader _this = (LcarsHeader)d;
                _this.HeaderContent = e.NewValue;
                _this.HeaderContentTemplate = _this.DefaultHeaderContentTemplate;
            }));

        public static readonly DependencyProperty HeaderContentProperty =
            DependencyProperty.Register(nameof(HeaderContent), typeof(object), typeof(LcarsHeader), new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderContentTemplateProperty =
            DependencyProperty.Register(nameof(HeaderContentTemplate), typeof(DataTemplate), typeof(LcarsHeader), new PropertyMetadata(null));        

        public static readonly DependencyProperty FrameReferenceProperty =
            DependencyProperty.Register(nameof(FrameReference), typeof(Frame), typeof(LcarsHeader), new PropertyMetadata(null));        
        
        public static readonly DependencyProperty IsBackButtonShownProperty =
            DependencyProperty.Register(nameof(IsBackButtonShown), typeof(bool), typeof(LcarsHeader), new PropertyMetadata(false, (d, e) =>
            {
                if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                {
                    return;
                }

                LcarsHeader _this = (LcarsHeader)d;
                bool newIsBackShown = (bool)e.NewValue;
                if (newIsBackShown)
                {
                    _this.FrameReference = App.Current.NavigationService.Frame;
                }
                else
                {
                    _this.FrameReference = null;
                    _this.BackButton.Visibility = Visibility.Collapsed;
                    _this.BackButtonCover.Visibility = Visibility.Collapsed;
                }
            }));

        public LcarsHeader()
        {            
            this.InitializeComponent();
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                return;
            }
        }

        bool animating = false;
        public async void PlayButtonAppearAnimation()
        {
            if(!IsBackButtonShown)
            {
                return;
            }

            if(animating)
            {
                return;
            }

            //setup
            animating = true;
            BackButton.Visibility = Visibility.Collapsed;
            BackButtonCover.Visibility = Visibility.Visible;
            await Task.WhenAll(
                BackButtonCover.Scale(0, 1, 0.5f, 0.5f, 0).StartAsync(),
                BackButtonCover.Fade(1, 0).StartAsync()
            );
            //Whoosh in the black cover
            await BackButtonCover.Scale(1, 1, (float)(BackButtonCover.ActualWidth / 2), (float)(BackButtonCover.ActualHeight / 2), 500).StartAsync();

            //fade in the back button by hiding the cover rectangle
            BackButton.Visibility = Visibility.Visible;
            await BackButtonCover.Fade(0, 200).StartAsync();

            //tear down                                   
            BackButtonCover.Visibility = Visibility.Collapsed;
            animating = false;
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public object HeaderContent
        {
            get { return (object)GetValue(HeaderContentProperty); }
            set { SetValue(HeaderContentProperty, value); }
        }

        public DataTemplate HeaderContentTemplate
        {
            get { return (DataTemplate)GetValue(HeaderContentTemplateProperty); }
            set { SetValue(HeaderContentTemplateProperty, value); }
        }

        /// <summary>
        /// A reference to the application <see cref="Frame"/>, used to feed the back button's NavButtonBehavior.
        /// </summary>
        public Frame FrameReference
        {
            get { return (Frame)GetValue(FrameReferenceProperty); }
            set { SetValue(FrameReferenceProperty, value); }
        }

        public bool IsBackButtonShown
        {
            get { return (bool)GetValue(IsBackButtonShownProperty); }
            set { SetValue(IsBackButtonShownProperty, value); }
        }
    }
}
