using System;
using System.Collections.Generic;
using DS.SimpleServiceBus.Events.Enums;
using DS.SimpleServiceBus.Events.Interfaces;
using DS.SimpleServiceBus.Extensions;

namespace DS.SimpleServiceBus.Events
{
    /// <inheritdoc />
    /// <summary>
    ///     Abstract class used for implementing an event.
    ///     The idea behind the ModelBefore property is to be able to see changes
    ///     made to the model when for instance sending events after commits in EF.
    ///     If the model supplied is a domain-object and the object is deleted through EF
    ///     the model in the event will be empty. The ModelBefore property will however
    ///     have a copy of the object before it was deleted, and the changes will list
    ///     all properties with a ValueBefore, and no ValueAfter.
    /// </summary>
    /// <typeparam name="TModel">Class implementing IModel</typeparam>
    public abstract class Event<TModel> : IEvent
        where TModel : IModel
    {
        private TModel _model;
        private TModel _modelBefore;
        private ICollection<ModelPropertyChange> _changes;

        protected Event() { }

        /// <summary>
        /// Sets the Model (and ModelBefore)
        /// </summary>
        /// <param name="setModel"></param>
        protected Event(Func<TModel> setModel)
        {
            if(setModel != null)
                Model = setModel();
        }

        /// <summary>
        ///     The model to use for this event
        /// </summary>
        public TModel Model
        {
            get => _model;
            set
            {
                _model = value;

                if(ModelBefore == null)
                    ModelBefore = value.DeepCopyByExpressionTree();
            }
        }

        /// <summary>
        ///     ModelBefore will always be set to a copy to prevent changes to the property
        /// </summary>
        public TModel ModelBefore {
            get => _modelBefore;
            set => _modelBefore = value.DeepCopyByExpressionTree();
        }

        /// <summary>
        ///     Get collection of changed properties between ModelBefore and ModelAfter
        /// </summary>
        public ICollection<ModelPropertyChange> Changes
        {
            get
            {
                GetChanges();
                return _changes;
            }
        }

        private void GetChanges()
        {
            if(_changes == null) _changes = new List<ModelPropertyChange>();
            _changes.Clear();

            if (ModelBefore == null) return;
            if (Model == null) return;

            foreach (var property in Model.GetType().GetProperties())
            {
                var oldValue = property.GetValue(ModelBefore, null);
                var newValue = property.GetValue(Model, null);

                if (!Equals(oldValue, newValue))
                {
                    _changes.Add(new ModelPropertyChange()
                    {
                        PropertyName = property.Name,
                        ValueBefore = oldValue,
                        ValueAfter = newValue,
                        Type = (oldValue == null && newValue != null)
                            ? ModelPropertyChangeTypeEnum.Add
                            : (oldValue != null && newValue == null)
                                ? ModelPropertyChangeTypeEnum.Delete
                                : ModelPropertyChangeTypeEnum.Change
                    });
                }
            }
        }
    }
}