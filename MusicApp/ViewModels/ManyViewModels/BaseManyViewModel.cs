using CommunityToolkit.Mvvm.Messaging;
using Microsoft.IdentityModel.Tokens;
using MusicApp.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MusicApp.ViewModels.ManyViewModels
{
    public abstract class BaseManyViewModel<ModelType, NewViewModel> : BaseDBViewModel where ModelType : class where NewViewModel : WorkspaceViewModel, new()
    {
        private ObservableCollection<ModelType> _Models;
        public ObservableCollection<ModelType> Models
        {
            get => _Models;
            set
            {
                if (_Models != value)
                {
                    _Models = value;
                    OnPropertyChanged(() => Models);
                }
            }
        }

        private ModelType? _SelectedItem;
        public ModelType? SelectedItem
        {
            get => _SelectedItem;
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    OnPropertyChanged(() => SelectedItem);
                }
                //SelectModel();
            }
        }

        private string? _SearchInput;
        public string? SearchInput
        {
            get => _SearchInput;
            set
            {
                if (_SearchInput != value)
                {
                    _SearchInput = value;
                    OnPropertyChanged(() => SearchInput);
                }
                SelectModel();
            }
        }
        public List<GenericComboBoxVM<string>> SearchandOrderColumns { get; set; }
        public string SearchColumn { get; set; }
        public string SortColumn { get; set; }
        public bool SortDescending { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand AddNewCommand { get; set; }
        public ICommand SelectCommand { get; set; }
        public BaseManyViewModel(string displayName) : base(displayName)
        {
            Refresh();
            RefreshCommand = new BaseCommand(() => Refresh());
            DeleteCommand = new BaseCommand(() => Delete());
            AddNewCommand = new BaseCommand(() => AddNew());
            SelectCommand = new BaseCommand(() => SelectModel());
            WeakReferenceMessenger.Default.Register<RefreshMessage<NewViewModel>>(this, (recipient, message) => Refresh());
            SearchandOrderColumns = GetSearchColumns();
            SearchColumn = SearchandOrderColumns.First().Key;
            SortColumn = SearchandOrderColumns.First().Key;
        }

        public abstract IQueryable<ModelType> GetModels();
        public abstract void DeleteFromDatabase();
        public abstract void SelectModel();
        public abstract void AddNew();
        public void Refresh()
        {
            IQueryable<ModelType> modelTypes = GetModels();
            if (!(SearchInput.IsNullOrEmpty()))
            {
                modelTypes = Search(modelTypes);
            }
            modelTypes = Sort(modelTypes);
            Models = new ObservableCollection<ModelType>(modelTypes);
        }

        private void Delete()
        {
            if (SelectedItem != null)
            {
                DeleteFromDatabase();
                Models.Remove(SelectedItem);
            }
        }
        /// <summary>
        /// Znajduje model w bazie danych, usuwa go i zapisuje zmiany
        /// </summary>
        protected abstract List<GenericComboBoxVM<string>> GetSearchColumns();

        protected abstract IQueryable<ModelType> Search(IQueryable<ModelType> models);
        protected abstract IQueryable<ModelType> Sort(IQueryable<ModelType> models);
    }
}
