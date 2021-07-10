using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace UIComponentsXF.Pages
{
    public partial class BaseNavigationPage : ContentPage
    {
        public View ChildContent
        {
            get { return this.ChildContentContainer.Content; }
            set { this.ChildContentContainer.Content = value; }
        }
        public Frame ModalFrame { get; set; }
        public Frame ModalBorderFrame { get; set; }
        public Frame OverlayFrame { get; set; }

        public BaseNavigationPage()
        {

            InitializeComponent();

            OverlayFrame = overlayFrame;
            ModalBorderFrame = modalBorderFrame;
            ModalFrame = modalFrame;

        }

        public void OnCloseModal(System.Object sender, System.EventArgs e)
        {
            ToogleModalVisibility(false);
        }

        public void ToogleModalVisibility(bool isVisible, View modalView = null) // order matters
        {
            if (isVisible)
            {
                OverlayFrame.IsVisible = isVisible;
                ModalBorderFrame.IsVisible = isVisible;
                ModalFrame.IsVisible = isVisible;
                ModalFrame.Content = modalView;
                return;

            }
            ModalFrame.IsVisible = isVisible;
            ModalBorderFrame.IsVisible = isVisible;
            OverlayFrame.IsVisible = isVisible;
        }
    }
}
