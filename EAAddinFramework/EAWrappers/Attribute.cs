﻿using System;
using System.Collections.Generic;
using System.Linq;

using UML=TSF.UmlToolingFramework.UML;

namespace TSF.UmlToolingFramework.Wrappers.EA {
  public class Attribute : Element, UML.Classes.Kernel.Property {
    private UML.Classes.Kernel.Type _type;
	internal global::EA.Attribute wrappedAttribute { get; set; }
    public int id
    {
    	get{return this.wrappedAttribute.AttributeID;}
    }

    public Attribute(Model model, global::EA.Attribute wrappedAttribute) 
      : base(model)
    {
      this.wrappedAttribute = wrappedAttribute;
    }

    public global::EA.Attribute WrappedAttribute
    {
    	get { return wrappedAttribute; }
    }
    
	public override List<UML.Classes.Kernel.Relationship> relationships {
		get 
		{
			string selectRelationsSQL = @"select c.Connector_ID from t_connector c
,t_attribute a where a.ea_guid = '"+this.wrappedAttribute.AttributeGUID +@"' 
and c.StyleEx like '%LF_P="+this.wrappedAttribute.AttributeGUID+"%'"
+@" and ((c.Start_Object_ID = a.Object_ID and c.End_Object_ID <> a.Object_ID)
    or (c.Start_Object_ID <> a.Object_ID and c.End_Object_ID = a.Object_ID))";
			return this.model.getRelationsByQuery(selectRelationsSQL).Cast<UML.Classes.Kernel.Relationship>().ToList();
    	}
		set { throw new NotImplementedException(); }
	}
    public bool isDerived {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public bool isDerivedUnion {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public bool isComposite {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public string _default {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public UML.Classes.Kernel.AggregationKind aggregation {
      get { return UML.Classes.Kernel.AggregationKind.none; }
      set { /* do nothing */ }
    }

    public UML.Classes.Kernel.ValueSpecification defaultValue {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public HashSet<UML.Classes.Kernel.Property> redefinedProperties {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public HashSet<UML.Classes.Kernel.Property> subsettedProperties {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public UML.Classes.Kernel.Property opposite {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public UML.Classes.Kernel.Classifier classifier {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public UML.Classes.Kernel.Class _class {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public UML.Classes.Kernel.Association owningAssociation {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public UML.Classes.Kernel.Association association {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public UML.Classes.Kernel.DataType datatype {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public bool isReadOnly {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    /// the isStatic property defines context of the attribute.
    /// If true then the context is the class
    /// If false then the context is the instance.
    public bool isStatic {
      get { return this.wrappedAttribute.IsStatic;  }
      set { this.wrappedAttribute.IsStatic = value; }
    }

    public HashSet<UML.Classes.Kernel.Classifier> featuringClassifiers {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public bool isLeaf {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public HashSet<UML.Classes.Kernel.RedefinableElement> redefinedElements {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public HashSet<UML.Classes.Kernel.Classifier> redefinitionContexts {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }

    public override string name {
      get { return this.wrappedAttribute.Name;  }
      set { this.wrappedAttribute.Name = value; }
    }
    public UML.Classes.Kernel.VisibilityKind visibility {
      get {
        return VisibilityKind.getUMLVisibilityKind
          ( this.wrappedAttribute.Visibility, 
            UML.Classes.Kernel.VisibilityKind._private );
      }
      set {
        this.wrappedAttribute.Visibility = 
          VisibilityKind.getEAVisibility(value);
      }
    }
    
    public String qualifiedName {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public UML.Classes.Kernel.Namespace owningNamespace {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public UML.Classes.Kernel.Type type 
    {
      	get 
      	{
    		if (this._type == null)
    		{
		        this._type = this.model.getElementWrapperByID( this.wrappedAttribute.ClassifierID ) as UML.Classes.Kernel.Type;
		        // check if the type is defined as an element in the model.
		        if(this.type == null ) 
		        {
		          // no element, create primitive type based on the name of the type
		          this._type = this.model.factory.createPrimitiveType(this.wrappedAttribute.Type);
		        }
    		}
        	return this._type;
      	}
      	set 
      	{
      		this._type = value;
	    	//set classifier if needed
	        if( value is ElementWrapper ) {
	          this.wrappedAttribute.ClassifierID = ((ElementWrapper)value).id;
	        }
	    	//always set type field
	        this.wrappedAttribute.Type = value.name;
      	}
    }
    
    public bool isOrdered {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public bool isUnique {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
	public UML.Classes.Kernel.UnlimitedNatural upper 
    {
      get {return this.multiplicity.upper;}
      set 
      { 
      	this.WrappedAttribute.UpperBound = value.ToString();
      }
    }

    public uint lower 
    {
      get { return this.multiplicity.lower ;}
      set 
      { 
      	this.WrappedAttribute.LowerBound = value.ToString();
      }
    }
    public Multiplicity multiplicity 
    {
    	get
    	{
    		//default for attributes is 1..1
    		string lowerString = "1";
    		string upperString = "1";
    		if (this.WrappedAttribute.LowerBound.Length > 0)
    		{
    			lowerString = this.WrappedAttribute.LowerBound;
    		}
    		if (this.WrappedAttribute.UpperBound.Length > 0)
    		{
    			upperString = this.WrappedAttribute.UpperBound;
    		}

    		return new Multiplicity(lowerString, upperString);
    	}
    	set
    	{
    		this.WrappedAttribute.LowerBound = value.lower.ToString();
    		this.WrappedAttribute.UpperBound = value.upper.ToString();
    	}
    }
    
    public UML.Classes.Kernel.ValueSpecification upperValue {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public UML.Classes.Kernel.ValueSpecification lowerValue {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public override HashSet<UML.Classes.Kernel.Element> ownedElements {
      get { return new HashSet<UML.Classes.Kernel.Element>(); }
      set { throw new NotImplementedException(); }
    }

    
    public override UML.Classes.Kernel.Element owner {
      get { return this.model.getElementWrapperByID(this.wrappedAttribute.ParentID);}
      set { throw new NotImplementedException(); }
    }
    
    public override HashSet<UML.Profiles.Stereotype> stereotypes {
      get {
        return ((Factory)this.model.factory).createStereotypes
          ( this, this.wrappedAttribute.StereotypeEx );
      }
      set 
      {
      	this.wrappedAttribute.StereotypeEx = Stereotype.getStereotypeEx(value);
      }
    }
    
    public bool isNavigable(){ throw new NotImplementedException(); }
    
    public List<UML.Classes.Dependencies.Dependency> clientDependencies {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }
    
    public List<UML.Classes.Dependencies.Dependency> supplierDependencies {
      get { throw new NotImplementedException(); }
      set { throw new NotImplementedException(); }
    }

    internal override void saveElement(){
      this.wrappedAttribute.Update();
    }

    public override String notes {
      get { return this.wrappedAttribute.Notes;  }
      set { this.wrappedAttribute.Notes = value; }
    }
    
    public bool _isNavigable {
      get { return true; }
      set { /* do nothing */ }
    }

  	
	public override TSF.UmlToolingFramework.UML.UMLItem getItemFromRelativePath(List<string> relativePath)
	{
		UML.UMLItem item = null;
		List<string> filteredPath = new List<string>(relativePath);
		if (ElementWrapper.filterName( filteredPath,this.name))
		{
	    	if (filteredPath.Count ==1)
	    	{
	    		item = this;
	    	}
		}
		return item; 
	}
	
	public override HashSet<UML.Profiles.TaggedValue> taggedValues
	{
		get 
		{
			return new HashSet<UML.Profiles.TaggedValue>(this.model.factory.createTaggedValues(this.wrappedAttribute.TaggedValues));
		}
		set { throw new NotImplementedException();}
	}
	public override HashSet<TSF.UmlToolingFramework.UML.Profiles.TaggedValue> getReferencingTaggedValues()
	{
		return this.model.getTaggedValuesWithValue(this.wrappedAttribute.AttributeGUID);
	}
	
	#region Equals and GetHashCode implementation
	public override bool Equals(object obj)
	{
		Attribute other = obj as Attribute;
		if (other != null)
		{
			if (other.wrappedAttribute.AttributeGUID == this.wrappedAttribute.AttributeGUID)
			{
				return true;	
			}
		}
		return false;
	}
	
	public override int GetHashCode()
	{
		return new Guid(this.wrappedAttribute.AttributeGUID).GetHashCode();
	}
	#endregion

  	
	internal override global::EA.Collection eaTaggedValuesCollection {
		get {
			return this.WrappedAttribute.TaggedValues;
		}
	}
  	
	public override string guid {
		get 
		{
			return this.WrappedAttribute.AttributeGUID;
		}
	}
  }
}
