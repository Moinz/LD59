using System;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;

namespace Seagulls
{
    /// <summary>
    ///     Observable class which spits out events when values change!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable, InlineProperty]
    public class Observable<T>
    {
        #region Serialized Fields

        [SerializeField, HideLabel] 
        private T _value;

        #endregion

        public event Action<T> OnChanged;

        /// <summary>
        /// Represents an observable object that emits events when its value changes.
        /// </summary>
        /// <typeparam name="T">The type of the value being observed.</typeparam>
        public Observable(T value)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the current value stored in the observable instance.
        /// When this value is changed using relevant methods, an event is triggered to notify listeners of the change.
        /// </summary>
        public T Value => _value;

        /// <summary>
        /// Initializes the value of the observable without triggering any change events.
        /// </summary>
        /// <param name="value">The initial value to be set.</param>
        public void SetWithoutNotify(T value)
        {
            _value = value;
        }

        /// <summary>
        /// Sets a new value for the observable and triggers the change event if the value is different.
        /// </summary>
        /// <param name="value">The new value to be assigned.</param>
        public void SetValue(T value)
        {
            if (EqualityComparer<T>.Default.Equals(_value, value))
                return;

            _value = value;
            OnChanged?.Invoke(_value);
        }

        /// <summary>
        /// Sets the value of the observable and forces a change event to be triggered,
        /// even if the new value matches the current value.
        /// </summary>
        /// <param name="value">The value to set and notify listeners about.</param>
        public void SetValueForced(T value)
        {
            _value = value;
            OnChanged?.Invoke(_value);
        }

        /// <summary>
        /// Implicitly converts an Observable<T> instance to its underlying value of type T.
        /// </summary>
        /// <param name="observable">The Observable<T> instance to convert.</param>
        /// <returns>The underlying value of type T held by the observable.</returns>
        public static implicit operator T(Observable<T> observable) => observable.Value;
        
        public override string ToString() => Value.ToString();
    }
}