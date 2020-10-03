using AsyncAwaitBestPractices.MVVM;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace XamarinMusicPlayer.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string errrorMessage;
        public string ErrorMessage
        {
            get => errrorMessage;
            set => SetProperty(ref errrorMessage, value);
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class BaseViewModel<T> : BaseViewModel where T : class
    {
        private ObservableCollection<T> collection;
        public ObservableCollection<T> Collection { get => collection; set => SetProperty(ref collection, value); }


        private T selectedItem;

        public T SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                SetProperty(ref selectedItem, value);
            }
        }

        public int Size { get => Collection.Count; }

        public IAsyncCommand RefreshCommand { get; private set; }

        private readonly Func<Task<IEnumerable<T>>> generator;

        public BaseViewModel(string title, Func<Task<IEnumerable<T>>> generator)
        {
            Title = title;
            this.generator = generator;
            RefreshCommand = new AsyncCommand(Process);
            RefreshCommand.ExecuteAsync();
        }

        private async Task Process()
        {
            var permissions = await HandlePermissions();
            if(!permissions)
            {
                ErrorMessage = "Storage Access is needed to acces the media files";
                return;
            }

            if (!IsBusy)
            {
                try
                {
                    IsBusy = true;
                    var collection = await generator().ConfigureAwait(false);
                    Collection = new ObservableCollection<T>(collection);
                }
                catch (Exception)
                {
                    ErrorMessage = $"Something went wrong while fetching {Title}";
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        private async Task<bool> HandlePermissions()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync<StoragePermission>();
            if (status == PermissionStatus.Granted)
                return true;
            else
            {
                var requestStatus = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
                return requestStatus == PermissionStatus.Granted;
            }
        }
    }
}
