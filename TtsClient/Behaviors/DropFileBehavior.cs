using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;
using TtsClient.ViewModels;

namespace TtsClient.Behaviors
{
    public class DropFileBehavior : Behavior<ListBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject == null)
            {
                return;
            }

            AssociatedObject.AllowDrop = true;
            AssociatedObject.DragOver += OnDragOver;
            AssociatedObject.Drop += OnDrop;
        }

        protected override void OnDetaching()
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.DragOver -= OnDragOver;
                AssociatedObject.Drop -= OnDrop;
            }

            base.OnDetaching();
        }

        private bool IsAllowedExtension(string path)
        {
            var extension = Path.GetExtension(path);
            return string.Equals(extension, ".yaml", StringComparison.OrdinalIgnoreCase);
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            try
            {
                var files = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (files == null || files.Length == 0)
                {
                    return;
                }

                var allowedFiles = files
                    .Where(f => !string.IsNullOrWhiteSpace(f) && IsAllowedExtension(f))
                    .OrderBy(f => f)
                    .Select(f => new FileListItem(f));

                if (AssociatedObject.ItemsSource is not ObservableCollection<FileListItem> list)
                {
                    return;
                }

                foreach (var fileListItem in allowedFiles)
                {
                    list.Add(fileListItem);
                }

                e.Handled = true;
            }
            catch
            {
                // ignore unexpected errors to avoid crashing on a drop
                Console.WriteLine("Error dropping files (DropFileBehavior.OnDrop)");
                e.Handled = true;
            }
        }
    }
}