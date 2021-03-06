﻿using System.Activities;
using System.Activities.Presentation.Metadata;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UiPath.Cryptography.Activities.Design.Properties;

namespace UiPath.Cryptography.Activities.Design
{
    public class DesignerMetadata : IRegisterMetadata
    {
        public void Register()
        {
            AttributeTableBuilder builder = new AttributeTableBuilder();

            builder.AddCustomAttributes(typeof(HashFile), new DesignerAttribute(typeof(HashFileActivityDesigner)));
            builder.AddCustomAttributes(typeof(HashText), new DesignerAttribute(typeof(HashTextActivityDesigner)));
            builder.AddCustomAttributes(typeof(KeyedHashFile), new DesignerAttribute(typeof(KeyedHashFileActivityDesigner)));
            builder.AddCustomAttributes(typeof(KeyedHashText), new DesignerAttribute(typeof(KeyedHashTextActivityDesigner)));
            builder.AddCustomAttributes(typeof(EncryptFile), new DesignerAttribute(typeof(EncryptFileActivityDesigner)));
            builder.AddCustomAttributes(typeof(EncryptText), new DesignerAttribute(typeof(EncryptTextActivityDesigner)));
            builder.AddCustomAttributes(typeof(DecryptFile), new DesignerAttribute(typeof(DecryptFileActivityDesigner)));
            builder.AddCustomAttributes(typeof(DecryptText), new DesignerAttribute(typeof(DecryptTextActivityDesigner)));

            // Categories
            CategoryAttribute cryptographyCategoryAttribute =
                new CategoryAttribute($"{Resources.CategorySystem}.{Resources.CategoryCryptography}");
            builder.AddCustomAttributes(typeof(HashFile), cryptographyCategoryAttribute);
            builder.AddCustomAttributes(typeof(HashText), cryptographyCategoryAttribute);
            builder.AddCustomAttributes(typeof(KeyedHashFile), cryptographyCategoryAttribute);
            builder.AddCustomAttributes(typeof(KeyedHashText), cryptographyCategoryAttribute);
            builder.AddCustomAttributes(typeof(EncryptFile), cryptographyCategoryAttribute);
            builder.AddCustomAttributes(typeof(EncryptText), cryptographyCategoryAttribute);
            builder.AddCustomAttributes(typeof(DecryptFile), cryptographyCategoryAttribute);
            builder.AddCustomAttributes(typeof(DecryptText), cryptographyCategoryAttribute);

            //DisplayName
            AddToAll(builder, typeof(HashFile).Assembly, nameof(Activity.DisplayName), new DisplayNameAttribute(Resources.DisplayName));

            MetadataStore.AddAttributeTable(builder.CreateTable());
        }

        private static void AddToAll(AttributeTableBuilder builder, Assembly asmb, string propName, DisplayNameAttribute attr)
        {
            var activities = asmb.GetExportedTypes().Where(a => typeof(Activity).IsAssignableFrom(a));
            foreach (var entry in activities)
            {
                builder.AddCustomAttributes(entry, propName, attr);
            }
        }
    }
}
