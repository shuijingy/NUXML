﻿#region Using Statements
using NUXML.ValueConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#endregion

namespace NUXML.Views.UI
{
    /// <summary>
    /// List view.
    /// </summary>
    /// <d>The list view presents a selectable list of items. It can either contain a static list of ListItem views or one ListItem with IsTemplate="True". If bound to list data through the Items field the list uses the template to generate a dynamic list of ListItems.</d>
    [HideInPresenter]
    public class List : UIView
    {
        #region Fields

        #region ListMask

        /// <summary>
        /// The width of the list mask image.
        /// </summary>
        /// <d>Specifies the width of the list mask image either in pixels or percents.</d>
        [MapTo("ListMask.Width")]
        public _ElementSize ListMaskWidth;

        /// <summary>
        /// The height of the list mask image.
        /// </summary>
        /// <d>Specifies the height of the list mask image either in pixels or percents.</d>
        [MapTo("ListMask.Height")]
        public _ElementSize ListMaskHeight;

        /// <summary>
        /// The offset of the list mask image.
        /// </summary>
        /// <d>Specifies the offset of the list mask image.</d>
        [MapTo("ListMask.Offset")]
        public _ElementMargin ListMaskOffset;

        /// <summary>
        /// List max image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered as the list max.</d>
        [MapTo("ListMask.BackgroundImage")]
        public _Sprite ListMaskImage;

        /// <summary>
        /// List max image type.
        /// </summary>
        /// <d>The type of the image sprite that is to be rendered as the list max.</d>
        [MapTo("ListMask.BackgroundImageType")]
        public _ImageType ListMaskImageType;

        /// <summary>
        /// List max image material.
        /// </summary>
        /// <d>The material of the list max image.</d>
        [MapTo("ListMask.BackgroundMaterial")]
        public _Material ListMaskMaterial;

        /// <summary>
        /// List max image color.
        /// </summary>
        /// <d>The color of the list max image.</d>
        [MapTo("ListMask.BackgroundColor")]
        public _Color ListMaskColor;

        /// <summary>
        /// List mask alignment.
        /// </summary>
        /// <d>Specifies the alignment of the list mask.</d>
        [MapTo("ListMask.Alignment")]
        public _ElementAlignment ListMaskAlignment;

        /// <summary>
        /// Indicates if list mask should be rendered.
        /// </summary>
        /// <d>Indicates if the list mask, i.e. the list mask background image sprite and color should be rendered.</d>
        [MapTo("ListMask.ShowMaskGraphic")]
        public _bool ListMaskShowGraphic;

        /// <summary>
        /// Content margin of the list.
        /// </summary>
        /// <d>Sets the margin of the list mask view that contains the contents of the list.</d>
        [MapTo("ListMask.Margin")]
        public _ElementMargin ContentMargin;

        /// <summary>
        /// List mask.
        /// </summary>
        /// <d>The list mask can be used to mask the list and its items using a mask graphic.</d>
        public Mask ListMask;

        #endregion

        /// <summary>
        /// User-defined data list.
        /// </summary>
        /// <d>Can be bound to an generic ObservableList to dynamically generate ListItems based on a template.</d>
        [ChangeHandler("ItemsChanged")]
        public _IObservableList Items;
       
        /// <summary>
        /// Orientation of the list.
        /// </summary>
        /// <d>Defines how the list items should be arranged.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementOrientation Orientation;

        /// <summary>
        /// Indicates if an item is selected.
        /// </summary>
        /// <d>Set to true when a list item is selected.</d>
        public _bool IsItemSelected;

        /// <summary>
        /// Indicates if items can be deselected by clicking.
        /// </summary>
        /// <d>A boolean indicating if items in the list can be deselected by clicking. Items can always be deselected programmatically.</d>
        public _bool CanDeselect;

        /// <summary>
        /// Indicates if more than one list item can be selected.
        /// </summary>
        /// <d>A boolean indicating if more than one list items can be selected by clicking or programmatically.</d>
        public _bool CanMultiSelect;

        /// <summary>
        /// Indicates if items can be selected by clicking.
        /// </summary>
        /// <d>A boolean indicating if items can be selected by clicking. Items can always be selected programmatically.</d>
        public _bool CanSelect;

        /// <summary>
        /// Indicates if items are deselected immediately after being selected.
        /// </summary>
        /// <d>A boolean indicating if items are deselected immediately after being selected. Useful if you want to trigger selection action but don't want the item to remain selected.</d>
        public _bool DeselectAfterSelect;

        /// <summary>
        /// Indicates how items overflow.
        /// </summary>
        /// <d>Enum indicating how items should overflow as they reach the boundaries of the list.</d>
        public _OverflowMode Overflow;

        /// <summary>
        /// Spacing between list items.
        /// </summary>
        /// <d>The spacing between list items.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementSize Spacing;

        /// <summary>
        /// Horizontal spacing between list items.
        /// </summary>
        /// <d>The horizontal spacing between list items.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementSize HorizontalSpacing;

        /// <summary>
        /// Vertical spacing between list items.
        /// </summary>
        /// <d>The vertical spacing between list items.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementSize VerticalSpacing;

        /// <summary>
        /// The alignment of list items.
        /// </summary>
        /// <d>If the list items varies in size the content alignment specifies how the list items should be arranged in relation to each other.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementAlignment ContentAlignment;

        /// <summary>
        /// Sort direction.
        /// </summary>
        /// <d>If list items has SortIndex set they can be sorted in the direction specified.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementSortDirection SortDirection;

        /// <summary>
        /// Indicates if items are selected on mouse up.
        /// </summary>
        /// <d>Boolean indicating if items are selected on mouse up rather than mouse down (default).</d>
        public _bool SelectOnMouseUp;

        /// <summary>
        /// Indicates if template is to be shown in the editor.
        /// </summary>
        /// <d>Boolean indicating if template should be shown in the editor.</d>
        public _bool ShowTemplateInEditor;        

        /// <summary>
        /// List item padding.
        /// </summary>
        /// <d>Adds padding to the list.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementMargin Padding;

        /// <summary>
        /// Selected data list item.
        /// </summary>
        /// <d>Set when the selected list item changes and points to the user-defined data item.</d>
        [ChangeHandler("SelectedItemChanged")]
        public _object SelectedItem;

        /// <summary>
        /// Selected items in the data list.
        /// </summary>
        /// <d>Contains selected items in the user-defined list data. Can contain more than one item if IsMultiSelect is true.</d>
        public _GenericObservableList SelectedItems;

        /// <summary>
        /// Item selected view action.
        /// </summary>
        /// <d>Triggered when a list item is selected either by user interaction or programmatically.</d>
        /// <actionData>ItemSelectionActionData</actionData>
        public ViewAction ItemSelected;

        /// <summary>
        /// Item deselected view action.
        /// </summary>
        /// <d>Triggered when a list item is deselected either by user interaction or programmatically. An item is deselected if another item is selected and CanMultiSelect is false. If CanMultiSelect is true an item is deselected when the user clicks on an selected item.</d>
        /// <actionData>ItemSelectionActionData</actionData>
        public ViewAction ItemDeselected;

        /// <summary>
        /// List changed view action.
        /// </summary>
        /// <d>Triggered when the list changes (items added, removed or moved).</d>
        /// <actionData>ListChangedActionData</actionData>
        public ViewAction ListChanged;

        private IObservableList _oldItems;
        private List<ListItem> _presentedListItems;
        private ListItem _listItemTemplate;
        private object _selectedItem;

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();
            Spacing.DirectValue = new ElementSize();
            CanSelect.DirectValue = true;
            CanDeselect.DirectValue = false;
            CanMultiSelect.DirectValue = false;
            Padding.DirectValue = new ElementMargin();
        }

        /// <summary>
        /// Called when a child layout has been updated.
        /// </summary>
        public override void ChildLayoutChanged()
        {
            base.ChildLayoutChanged();
            QueueChangeHandler("LayoutChanged");
        }

        /// <summary>
        /// Updates the layout of the view.
        /// </summary>
        public override void LayoutChanged()
        {
#if UNITY_EDITOR
            // if ShowTemplateInEditor is set and we are in editor the template may be visible
            if (ShowTemplateInEditor && Application.isEditor)
            {
                // hide template if we have any items to present
                if (ListItemTemplate != null && _presentedListItems.Count > 0)
                {
                    ListItemTemplate.Deactivate();
                }
            }
#endif

            // arrange items like a group
            float horizontalSpacing = HorizontalSpacing.IsSet ? HorizontalSpacing.Value.Pixels : Spacing.Value.Pixels;
            float verticalSpacing = VerticalSpacing.IsSet ? VerticalSpacing.Value.Pixels : Spacing.Value.Pixels;
            float maxWidth = 0f;
            float maxHeight = 0f;
            float totalWidth = 0f;
            float totalHeight = 0f;
            bool percentageWidth = false;
            bool percentageHeight = false;

            bool isHorizontal = Orientation == ElementOrientation.Horizontal;

            var children = new List<ListItem>();
            var childrenToBeSorted = new List<ListItem>();
            Content.ForEachChild<ListItem>(x =>
            {
                // should this be sorted?
                if (x.SortIndex != 0)
                {
                    // yes. 
                    childrenToBeSorted.Add(x);
                    return;
                }

                children.Add(x);
            }, false);

            if (SortDirection == ElementSortDirection.Ascending)
            {
                children.AddRange(childrenToBeSorted.OrderBy(x => x.SortIndex.Value));
            }
            else
            {
                children.AddRange(childrenToBeSorted.OrderByDescending(x => x.SortIndex.Value));
            }

            // get size of content and set content offsets and alignment
            int childCount = children.Count;
            int childIndex = 0;
            bool firstItem = true;
            float xOffset = 0;
            float yOffset = 0;
            float maxColumnWidth = 0;
            float maxRowHeight = 0;
            
            for (int i = 0; i < childCount; ++i)
            {
                var view = children[i];

                // don't group disabled or destroyed views
                if (!view.IsActive || view.IsDestroyed)
                    continue;

                if (view.Width.Value.Unit == ElementSizeUnit.Percents)
                {
                    if (isHorizontal)
                    {
                        Debug.LogWarning(String.Format("[NUXML] Unable to group view \"{0}\" horizontally as it doesn't specify its width in pixels or elements.", view.GameObjectName));
                        continue;
                    }
                    else
                    {
                        percentageWidth = true;
                    }
                }

                if (view.Height.Value.Unit == ElementSizeUnit.Percents)
                {
                    if (!isHorizontal)
                    {
                        Debug.LogWarning(String.Format("[NUXML] Unable to group view \"{0}\" vertically as it doesn't specify its height in pixels or elements.", view.GameObjectName));
                        continue;
                    }
                    else
                    {
                        percentageHeight = true;
                    }
                }
                                                                
                if (Overflow == OverflowMode.Overflow)
                {
                    // set offsets and alignment
                    var offset = new ElementMargin(
                        new ElementSize(isHorizontal ? totalWidth + horizontalSpacing * childIndex : 0f, ElementSizeUnit.Pixels),
                        new ElementSize(!isHorizontal ? totalHeight + verticalSpacing * childIndex : 0f, ElementSizeUnit.Pixels));
                    view.OffsetFromParent.DirectValue = offset;

                    // set desired alignment if it is valid for the orientation otherwise use defaults                
                    var alignment = isHorizontal ? ElementAlignment.Left : ElementAlignment.Top;
                    var desiredAlignment = ContentAlignment.IsSet ? ContentAlignment : view.Alignment;
                    if (isHorizontal && (desiredAlignment == ElementAlignment.Top || desiredAlignment == ElementAlignment.Bottom
                        || desiredAlignment == ElementAlignment.TopLeft || desiredAlignment == ElementAlignment.BottomLeft))
                    {
                        view.Alignment.DirectValue = alignment | desiredAlignment;
                    }
                    else if (!isHorizontal && (desiredAlignment == ElementAlignment.Left || desiredAlignment == ElementAlignment.Right
                        || desiredAlignment == ElementAlignment.TopLeft || desiredAlignment == ElementAlignment.TopRight))
                    {
                        view.Alignment.DirectValue = alignment | desiredAlignment;
                    }
                    else
                    {
                        view.Alignment.DirectValue = alignment;
                    }

                    // get size of content
                    if (!percentageWidth)
                    {
                        totalWidth += view.Width.Value.Pixels;
                        maxWidth = Mathf.Max(maxWidth, view.Width.Value.Pixels);
                    }

                    if (!percentageHeight)
                    {
                        totalHeight += view.Height.Value.Pixels;
                        maxHeight = Mathf.Max(maxHeight, view.Height.Value.Pixels);
                    }
                }
                else
                {
                    // overflow mode is set to wrap
                    // set alignment
                    view.Alignment.DirectValue = ElementAlignment.TopLeft;

                    // set offsets of item
                    if (isHorizontal)
                    {
                        if (firstItem)
                        {
                            // first item, don't check for overflow
                            xOffset = 0;
                            firstItem = false;
                        }
                        else if ((xOffset + view.Width.Value.Pixels + horizontalSpacing) > ActualWidth)
                        {
                            // item overflows to the next row
                            xOffset = 0;
                            yOffset += maxRowHeight + verticalSpacing;
                            maxRowHeight = 0;
                        }
                        else
                        {
                            // item continues on the same row
                            xOffset += horizontalSpacing;
                        }

                        // set offset
                        view.OffsetFromParent.DirectValue = new ElementMargin(ElementSize.FromPixels(xOffset), ElementSize.FromPixels(yOffset));
                        xOffset += view.Width.Value.Pixels;
                        maxRowHeight = Mathf.Max(maxRowHeight, view.Height.Value.Pixels);
                        maxWidth = Mathf.Max(maxWidth, xOffset);
                        maxHeight = Mathf.Max(maxHeight, yOffset + view.Height.Value.Pixels);
                    }
                    else
                    {
                        if (firstItem)
                        {
                            yOffset = 0;
                            firstItem = false;
                        }
                        else if ((yOffset + view.Height.Value.Pixels + verticalSpacing) > ActualHeight)
                        {
                            // overflow to next column                        
                            yOffset = 0;
                            xOffset += maxColumnWidth + horizontalSpacing;
                            maxColumnWidth = 0;
                        }
                        else
                        {
                            // add spacing
                            yOffset += verticalSpacing;
                        }

                        // set offset
                        view.OffsetFromParent.DirectValue = new ElementMargin(ElementSize.FromPixels(xOffset), ElementSize.FromPixels(yOffset));
                        yOffset += view.Height.Value.Pixels;
                        maxColumnWidth = Mathf.Max(maxColumnWidth, view.Width.Value.Pixels);
                        maxWidth = Mathf.Max(maxWidth, xOffset + view.Width.Value.Pixels);
                        maxHeight = Mathf.Max(maxHeight, yOffset);
                    }
                }

                // update child layout
                view.LayoutChanged();
                ++childIndex;
            }


            if (Overflow == OverflowMode.Overflow)
            {
                // set width and height of list            
                if (!Width.IsSet)
                {
                    // if width is not explicitly set then adjust to content
                    if (!percentageWidth)
                    {
                        // add margins
                        totalWidth += isHorizontal ? (childCount > 1 ? (childIndex - 1) * horizontalSpacing : 0f) : 0f;
                        totalWidth += Margin.Value.Left.Pixels + Margin.Value.Right.Pixels + ListMask.Margin.Value.Left.Pixels + ListMask.Margin.Value.Right.Pixels
                            + Padding.Value.Left.Pixels + Padding.Value.Right.Pixels;
                        maxWidth += Margin.Value.Left.Pixels + Margin.Value.Right.Pixels + ListMask.Margin.Value.Left.Pixels + ListMask.Margin.Value.Right.Pixels
                            + Padding.Value.Left.Pixels + Padding.Value.Right.Pixels;

                        // adjust width to content
                        Width.DirectValue = new ElementSize(isHorizontal ? totalWidth : maxWidth, ElementSizeUnit.Pixels);
                    }
                    else
                    {
                        Width.DirectValue = new ElementSize(1, ElementSizeUnit.Percents);
                    }
                }

                if (!Height.IsSet)
                {
                    // if height is not explicitly set then adjust to content
                    if (!percentageHeight)
                    {
                        // add margins
                        totalHeight += !isHorizontal ? (childCount > 1 ? (childIndex - 1) * verticalSpacing : 0f) : 0f;
                        totalHeight += Margin.Value.Top.Pixels + Margin.Value.Bottom.Pixels + ListMask.Margin.Value.Top.Pixels + ListMask.Margin.Value.Bottom.Pixels
                            + Padding.Value.Top.Pixels + Padding.Value.Bottom.Pixels;
                        maxHeight += Margin.Value.Top.Pixels + Margin.Value.Bottom.Pixels + ListMask.Margin.Value.Top.Pixels + ListMask.Margin.Value.Bottom.Pixels
                            + Padding.Value.Top.Pixels + Padding.Value.Bottom.Pixels;

                        // adjust height to content
                        Height.DirectValue = new ElementSize(!isHorizontal ? totalHeight : maxHeight, ElementSizeUnit.Pixels);
                    }
                    else
                    {
                        Height.DirectValue = new ElementSize(1, ElementSizeUnit.Percents);
                    }
                }
            }
            else
            {           
                // adjust size to content
                if (isHorizontal)
                {
                    maxHeight += Margin.Value.Top.Pixels + Margin.Value.Bottom.Pixels + ListMask.Margin.Value.Top.Pixels + 
                        ListMask.Margin.Value.Bottom.Pixels + Padding.Value.Top.Pixels + Padding.Value.Bottom.Pixels;
                    Height.DirectValue = ElementSize.FromPixels(maxHeight);
                }
                else
                {
                    maxWidth += Margin.Value.Left.Pixels + Margin.Value.Right.Pixels + ListMask.Margin.Value.Left.Pixels + 
                        ListMask.Margin.Value.Right.Pixels + Padding.Value.Left.Pixels + Padding.Value.Right.Pixels;
                    Width.DirectValue = ElementSize.FromPixels(maxWidth);
                }
            }

            base.LayoutChanged();
        }

        /// <summary>
        /// Called when the selected item of the list has been changed.
        /// </summary>
        public virtual void SelectedItemChanged()
        {
            if (_selectedItem == SelectedItem.Value)
            {
                return;
            }

            SelectItem(SelectedItem.Value);
        }

        /// <summary>
        /// Selects item in the list.
        /// </summary>
        public void SelectItem(ListItem listItem, bool triggeredByClick = false)
        {
            if (listItem == null || (triggeredByClick && !CanSelect))
                return;
                        
            // is item already selected?
            if (listItem.IsSelected)
            {
                // yes. can it be deselected?
                if (triggeredByClick && !CanDeselect)
                    return; // no.

                // deselect and trigger actions
                SetSelected(listItem, false);
            }
            else
            {
                // select
                SetSelected(listItem, true);

                // deselect other items if we can't multi-select
                if (!CanMultiSelect)
                {
                    foreach (var presentedListItem in _presentedListItems)
                    {
                        if (presentedListItem == listItem)
                            continue;

                        // deselect and trigger actions
                        SetSelected(presentedListItem as ListItem, false);
                    }
                }

                // should this item immediately be deselected?
                if (DeselectAfterSelect)
                {
                    // yes.
                    SetSelected(listItem, false); 
                }
            }            
        }

        /// <summary>
        /// Selects item in the list.
        /// </summary>
        public void SelectItem(int index)
        {
            if (index >= _presentedListItems.Count)
            {
                Debug.LogError(String.Format("[NUXML] {0}: Unable to select list item. Index out of bounds.", GameObjectName));
                return;
            }

            SelectItem(_presentedListItems[index] as ListItem, false);
        }

        /// <summary>
        /// Selects item in the list.
        /// </summary>
        public void SelectItem(object itemData)
        {
            var listItem = _presentedListItems.FirstOrDefault(x =>
            {
                var item = x as ListItem;
                return item != null ? item.Item.Value == itemData : false;
            });

            if (listItem == null)
            {
                Debug.LogError(String.Format("[NUXML] {0}: Unable to select list item. Item not found.", GameObjectName));
                return;
            }

            SelectItem(listItem as ListItem, false);
        }

        /// <summary>
        /// Selects or deselects a list item.
        /// </summary>
        private void SetSelected(ListItem listItem, bool selected)
        {
            if (listItem == null)
                return;

            listItem.IsSelected.Value = selected;
            if (selected)
            {
                // item selected
                _selectedItem = listItem.Item.Value;
                SelectedItem.Value = _selectedItem;                
                IsItemSelected.Value = true;
                if (Items.Value != null)
                {
                    Items.Value.SetSelected(_selectedItem);
                }

                // add to list of selected items
                SelectedItems.Value.Add(listItem.Item.Value);

                // trigger item selected action
                if (ItemSelected.HasEntries)
                {
                    ItemSelected.Trigger(new ItemSelectionActionData { IsSelected = true, ItemView = listItem, Item = listItem.Item.Value });
                }
            }
            else
            {
                // remove from list of selected items
                SelectedItems.Value.Remove(listItem.Item.Value);

                // set selected item
                if (SelectedItem.Value == listItem.Item.Value)
                {
                    _selectedItem = SelectedItems.Value.LastOrDefault();
                    SelectedItem.Value = _selectedItem;

                    if (Items.Value != null)
                    {
                        Items.Value.SetSelected(_selectedItem);
                    }
                }
                IsItemSelected.Value = SelectedItems.Value.Count > 0;

                // trigger item deselected action
                if (ItemDeselected.HasEntries)
                {
                    ItemDeselected.Trigger(new ItemSelectionActionData { IsSelected = selected, ItemView = listItem, Item = listItem.Item.Value });
                }
            }
        }

        /// <summary>
        /// Called when the list of items has been changed.
        /// </summary>
        public virtual void ItemsChanged()
        {
            if (ListItemTemplate == null)
                return; // static list 

            Rebuild();
            LayoutsChanged();
        }

        /// <summary>
        /// Called when the list of items has been changed.
        /// </summary>
        private void OnListChanged(object sender, ListChangedEventArgs e)
        {
            // update list of items
            if (e.ListChangeAction == ListChangeAction.Clear)
            {
                Clear();
            }
            else if (e.ListChangeAction == ListChangeAction.Add)
            {
                AddRange(e.StartIndex, e.EndIndex);
            }
            else if (e.ListChangeAction == ListChangeAction.Remove)
            {
                RemoveRange(e.StartIndex, e.EndIndex);
            }
            else if (e.ListChangeAction == ListChangeAction.Modify)
            {
                ItemsModified(e.StartIndex, e.EndIndex, e.FieldPath);
            }
            else if (e.ListChangeAction == ListChangeAction.Select)
            {
                SelectItem(e.StartIndex);
            }
            else if (e.ListChangeAction == ListChangeAction.Replace)
            {
                ItemsReplaced(e.StartIndex, e.EndIndex);
            }
            else if (e.ListChangeAction == ListChangeAction.Move)
            {
            }

            if (ListChanged.HasEntries)
            {
                ListChanged.Trigger(new ListChangedActionData { ListChangeAction = e.ListChangeAction, StartIndex = e.StartIndex, EndIndex = e.EndIndex, FieldPath = e.FieldPath });
            }
                        
            // update sort index
            UpdateSortIndex();
            LayoutsChanged();
        }

        /// <summary>
        /// Updates the sort index on the list items.
        /// </summary>
        public void UpdateSortIndex()
        {
            int index = 1;
            Content.ForEachChild<UIView>(x =>
            {
                if (!x.IsLive)
                    return;

                int itemIndex = Items.Value != null ? Items.Value.GetIndex(x.Item.Value) : index;
                x.SortIndex.DirectValue = itemIndex;
                ++index;
            }, false);
        }

        /// <summary>
        /// Rebuilds the entire list.
        /// </summary>
        public void Rebuild()
        {
            // assume a completely new list has been set
            if (_oldItems != null)
            {
                // unsubscribe from change events in the old list
                _oldItems.ListChanged -= OnListChanged;
            }
            _oldItems = Items.Value;

            // clear list
            Clear();

            // add new list
            if (Items.Value != null)
            {
                // subscribe to change events in the new list
                Items.Value.ListChanged += OnListChanged;

                // add list items
                if (Items.Value.Count > 0)
                {
                    AddRange(0, Items.Value.Count - 1);
                }
            }

            // update sort index
            UpdateSortIndex();
        }

        /// <summary>
        /// Clears the list items.
        /// </summary>
        public void Clear()
        {
            foreach (var presentedItem in _presentedListItems)
            {
                DestroyListItem(presentedItem);
            }

            _presentedListItems.Clear();
        }

        /// <summary>
        /// Adds a range of list items.
        /// </summary>
        private void AddRange(int startIndex, int endIndex)
        {
            if (Items == null)
                return;

            // make sure we have a template
            if (ListItemTemplate == null)
            {
                Debug.LogError(String.Format("[NUXML] {0}: Unable to generate list from items. Template missing. Add a template by adding a view with IsTemplate=\"True\" to the list.", GameObjectName));
                return;
            }

            // validate input
            int lastIndex = Items.Value.Count - 1;
            int insertCount = (endIndex - startIndex) + 1;
            bool listMatch = _presentedListItems.Count == (Items.Value.Count - insertCount);
            if (startIndex < 0 || startIndex > lastIndex || 
                endIndex < startIndex || endIndex > lastIndex || !listMatch)
            {
                Debug.LogWarning(String.Format("[NUXML] {0}: List mismatch. Rebuilding list.", GameObjectName));
                Rebuild();
                return;
            }

            // insert items
            for (int i = startIndex; i <= endIndex; ++i)
            {
                CreateListItem(i);
            }
        }

        /// <summary>
        /// Removes a range of list items.
        /// </summary>
        private void RemoveRange(int startIndex, int endIndex)
        {
            // validate input
            int lastIndex = _presentedListItems.Count - 1;
            int removeCount = (endIndex - startIndex) + 1;
            bool listMatch = _presentedListItems.Count == (Items.Value.Count + removeCount);
            if (startIndex < 0 || startIndex > lastIndex ||
                endIndex < startIndex || endIndex > lastIndex || !listMatch)
            {
                Debug.LogWarning(String.Format("[NUXML] {0}: List mismatch. Rebuilding list.", GameObjectName));
                Rebuild();
                return;
            }

            // remove items
            for (int i = endIndex; i >= startIndex; --i)
            {
                DestroyListItem(i);
            }
        }

        /// <summary>
        /// Called when item data in the list have been modified.
        /// </summary>
        private void ItemsModified(int startIndex, int endIndex, string fieldPath = "")
        {
            // validate input
            int lastIndex = _presentedListItems.Count - 1;
            bool listMatch = _presentedListItems.Count == Items.Value.Count;
            if (startIndex < 0 || startIndex > lastIndex || endIndex < startIndex || endIndex > lastIndex || !listMatch)
            {
                Debug.LogWarning(String.Format("[NUXML] {0}: List mismatch. Rebuilding list.", GameObjectName));
                Rebuild();
                return;
            }

            // notify observers that item has changed
            for (int i = startIndex; i <= endIndex; ++i)
            {
                ItemModified(i, fieldPath);
            }
        }

        /// <summary>
        /// Called when item data in list has been modified.
        /// </summary>
        private void ItemModified(int index, string fieldPath = "")
        {
            object itemData = Items.Value[index];
            var listItem = _presentedListItems[index];
            var path = String.IsNullOrEmpty(fieldPath) ? "Item" : "Item." + fieldPath;

            listItem.ForThisAndEachChild<UIView>(x =>
            {
                // TODO can be made faster if a HasItemBinding flag is implemented, also we can stop traversing the tree if another item is set
                if (x.Item.Value == itemData)
                {
                    x.NotifyDependentValueObservers(path, true);
                }
            });
        }

        /// <summary>
        /// Called when item data in the list have been replaced.
        /// </summary>
        private void ItemsReplaced(int startIndex, int endIndex)
        {
            // validate input
            int lastIndex = _presentedListItems.Count - 1;
            bool listMatch = _presentedListItems.Count == Items.Value.Count;
            if (startIndex < 0 || startIndex > lastIndex || endIndex < startIndex || endIndex > lastIndex || !listMatch)
            {
                Debug.LogWarning(String.Format("[NUXML] {0}: List mismatch. Rebuilding list.", GameObjectName));
                Rebuild();
                return;
            }

            // replace items
            for (int i = startIndex; i <= endIndex; ++i)
            {
                ItemReplaced(i);
            }
        }

        /// <summary>
        /// Called when item data in list has been replaced.
        /// </summary>
        private void ItemReplaced(int index)
        {
            object newItemData = Items.Value[index];
            var listItem = _presentedListItems[index];
            var oldItemData = listItem.Item.Value;
            
            listItem.ForThisAndEachChild<UIView>(x =>
            {
                // TODO can be made faster if a HasItemBinding flag is implemented, also we can stop traversing the tree if another item is set
                if (x.Item.Value == oldItemData)
                {
                    x.Item.Value = newItemData;
                    x.NotifyDependentValueObservers("Item", true);
                }
            });
        }

        /// <summary>
        /// Creates and initializes a new list item.
        /// </summary>
        private ListItem CreateListItem(int index)
        {
            object itemData = Items.Value[index];
            var newItemView = Content.CreateView(ListItemTemplate, index + 1);
            _presentedListItems.Insert(index, newItemView);
                        
            // set item data
            newItemView.ForThisAndEachChild<UIView>(x =>
            {
                if (x.FindParent<List>() == this)
                {
                    x.Item.Value = itemData;
                }
            });
            newItemView.Activate();

            // initialize view
            newItemView.InitializeViews();            

            return newItemView;
        }
        
        /// <summary>
        /// Destroys a list item.
        /// </summary>
        private void DestroyListItem(int index)
        {
            var itemView = _presentedListItems[index];
            DestroyListItem(itemView);
            _presentedListItems.RemoveAt(index);
        }

        /// <summary>
        /// Destroys a list item.
        /// </summary>
        private void DestroyListItem(ListItem presentedItem)
        {
            // deselect the item first
            SetSelected(presentedItem, false);

            presentedItem.IsDestroyed.DirectValue = true;
            if (Application.isPlaying)
            {
                GameObject.Destroy(presentedItem.gameObject);
            }
            else
            {
                GameObject.DestroyImmediate(presentedItem.gameObject);
            }
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            
            _presentedListItems = new List<ListItem>();
            if (ListItemTemplate != null)
            {
#if UNITY_EDITOR
                // deactivate if ShowTemplateInEditor is false or we are outside the editor
                if (!ShowTemplateInEditor || !Application.isEditor)
                {
                    ListItemTemplate.Deactivate();
                }
#else
                ListItemTemplate.Deactivate();
#endif
            }

            UpdatePresentedListItems();            
            SelectedItems.DirectValue = new GenericObservableList();
        }

        /// <summary>
        /// Updates list of presented list items. Needs to be called after list items are added manually to the list.
        /// </summary>
        public void UpdatePresentedListItems()
        {
            _presentedListItems.Clear();
            _presentedListItems.AddRange(Content.GetChildren<ListItem>(x => !x.IsTemplate && !x.IsDestroyed, false));
            UpdateSortIndex();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns list item template.
        /// </summary>
        public ListItem ListItemTemplate
        {
            get
            {
                if (!_listItemTemplate)
                {
                    _listItemTemplate = Content.Find<ListItem>(x => x.IsTemplate, false);
                }

                return _listItemTemplate;
            }
        }
        
        #endregion
    }
}
