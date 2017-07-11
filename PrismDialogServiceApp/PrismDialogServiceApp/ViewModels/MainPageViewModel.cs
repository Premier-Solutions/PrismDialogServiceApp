using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PrismDialogServiceApp.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        IPageDialogService _pageDialogService;

        public DelegateCommand DisplayAlertCommand { get; set; }
        public DelegateCommand DisplayActionSheetCommand { get; set; }
        public DelegateCommand DisplayActionSheetCommand2 { get; set; }

        public MainPageViewModel(IPageDialogService pageDialogService)
        {
            _pageDialogService = pageDialogService;

            DisplayAlertCommand = new DelegateCommand(DisplayAlert);
            DisplayActionSheetCommand = new DelegateCommand(DisplayActionSheet);
            DisplayActionSheetCommand2 = new DelegateCommand(DisplayActionSheet2);
        }

        private async void DisplayAlert()
        {
            bool result = await _pageDialogService.DisplayAlertAsync("Alert", "Alert From MainPageViewModel.", "Accept", "Cancel");
            Debug.WriteLine(result);
        }

        private async void DisplayActionSheet()
        {
            var result = await _pageDialogService.DisplayActionSheetAsync("ActionSheet", "Cancel", "Destroy", "Option 1", "Option 2");
            Debug.WriteLine(result);
        }

        private async void DisplayActionSheet2()
        {
            IActionSheetButton option1Action = ActionSheetButton.CreateButton("Option 1", new DelegateCommand(() => { Debug.WriteLine("Option 1"); }));
            IActionSheetButton option2Action = ActionSheetButton.CreateButton("Option 2", new DelegateCommand(() => { Debug.WriteLine("Option 2"); }));
            IActionSheetButton cancelAction = ActionSheetButton.CreateCancelButton("Cancel", new DelegateCommand(() => { Debug.WriteLine("Cancel"); }));
            IActionSheetButton destroyAction = ActionSheetButton.CreateDestroyButton("Destroy", new DelegateCommand(() => { Debug.WriteLine("Destroy"); }));

            await _pageDialogService.DisplayActionSheetAsync("ActionSheet with ActionSheetButtons", option1Action, option2Action, cancelAction, destroyAction);
        }

    }
}
