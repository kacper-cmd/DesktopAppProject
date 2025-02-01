using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MusicApp.ViewModels.SingleViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Model Type From Database (From model layer) eg. Song</typeparam>
    public abstract class BaseSingleViewModel<T> : BaseDBViewModel where T : class
    {
        #region FieldsAndProperties
        private T? _SelectedItem;
        public T? SelectedItem
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
        public ICommand SaveCommand { get; set; }
        public ICommand SelectCommand { get; set; }

        public T Model { get; set; }
        #endregion


        #region Constructors
        public BaseSingleViewModel(string displayName) : base(displayName)
        {
            SaveCommand = new BaseCommand(() => Save());
            SelectCommand = new BaseCommand(() => Select());
            Model = InitializeModel();
            GetDBTable().Add(Model);
        }
        public BaseSingleViewModel(int id, string displayName) : base(displayName)
        {
            SaveCommand = new BaseCommand(() => Save());
            SelectCommand = new BaseCommand(() => Select());
            Model = GetModelFromDatabase(id) ?? InitializeModel();
        }
        #endregion

        #region Methods
        private void Save()
        {
            string? error = ValidateModel();
            if (error.IsNullOrEmpty())
            {
                try
                {
                    Database.SaveChanges();
                    WeakReferenceMessenger.Default.Send<RefreshMessage<T>>();
                    OnRequestClose();
                }
                catch (SqlException ex)
                {

                }
                catch (DbUpdateException dbex)
                {

                }
            }
            else
            {
                MessageBox.Show(error);
            }
        }

        protected abstract void Select();
        protected abstract DbSet<T> GetDBTable();
        protected abstract T InitializeModel();
        protected abstract T? GetModelFromDatabase(int id);
        protected abstract string? ValidateProperty(string propertyName);
        protected virtual string? ValidateModel()
        {
            string output = string.Empty;

            IEnumerable<string?> enumerable = this.GetType().GetProperties().Select(item => ValidateProperty(item.Name)).Where(item => item != null);

            return string.Join("\n", enumerable);

        }
        #endregion
    }
}
