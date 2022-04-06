using DragAndDrop.Models;
using DragAndDrop.Views;
using System;
using Xamarin.Forms;

namespace DragAndDrop.TemplateSelectors
{
    public class TypeModelTemplateSelector : DataTemplateSelector
    {
        private DataTemplate TypeOneTemplate = new DataTemplate(() => new ViewCell { View = new TypeOneView() });
        private DataTemplate TypeTwoTemplate = new DataTemplate(() => new ViewCell { View = new TypeTwoView() });

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        //    => item switch 
        //{
        //    TypeOneModel _ => TypeOneTemplate,
        //    TypeTwoModel _ => TypeTwoTemplate,
        //    _ => throw new NotSupportedException()
        //};
        {
            if (item is TypeOneModel)
            {
                return TypeOneTemplate;
            }
            
            if (item is TypeTwoModel)
            {
                return TypeTwoTemplate;
            }

            throw new NotSupportedException();
        }
    }
}
