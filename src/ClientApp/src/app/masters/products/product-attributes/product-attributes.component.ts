import { Component, OnInit,Inject,ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { NgbModal,NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import Swal from 'sweetalert2';


import { ProductProperty } from '../../../models/ProductProperty';
import { Unit } from '../../../models/Unit';
import { Product } from '../../../models/Product';
import { ProductAttribute } from '../../../models/ProductAttribute';
@Component({
  selector: 'app-product-attributes',
  templateUrl: './product-attributes.component.html',
  styleUrls: ['./product-attributes.component.css']
})
export class ProductAttributesComponent implements OnInit {
  modalReference: NgbModalRef;
  btnSubmited :boolean= false;
  baseUrl: string;

  properties: Array<ProductProperty> = new Array<ProductProperty>();
  units: Array<Unit> = new Array<Unit>();
  products:Array<Product> = new Array<Product>();
  productAttributes:Array<ProductAttribute> = new Array<ProductAttribute>();
  productAttribute:ProductAttribute = new ProductAttribute();

  productAttributeForm= new FormGroup({
    productId: new FormControl(null, Validators.required), 
    propertyId: new FormControl(null, Validators.required),
    propertyValue: new FormControl('', Validators.required),
    unitValue: new FormControl('', Validators.required),
    mrp: new FormControl(null, Validators.required),
    rate: new FormControl(null, Validators.required),
    status: new FormControl(null, Validators.required),
  });
  displayedColumns: string[] = ['Product','Property','PropertyValue','MRP','Rate','Status','Actions'];
  dataSource: MatTableDataSource<ProductAttribute>; 
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private http: HttpClient, 
    @Inject('BASE_URL') url: string,
   public dialog: MatDialog,
   private modalService: NgbModal) { 
    this.baseUrl=url;
 }

  ngOnInit() {
    this.getProductAttributes();
    this.getProducts();
    this.getProductProperties();
    this.getUnits();
  }
  getProductAttributes() {
    this.http.get<Array<ProductAttribute>>(this.baseUrl + 'api/product/productAttributes').subscribe(result => {
      this.productAttributes = result;
      this.dataSource = new MatTableDataSource(this.productAttributes);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }, error => console.error(error));
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  getProducts() {
    this.http.get<Array<Product>>(this.baseUrl + 'api/product').subscribe(result => {
      this.products = result.filter(t=>t.isActive ===true);
    }, error => console.error(error));
  }
  getProductProperties() {
    this.http.get<Array<ProductProperty>>(this.baseUrl + 'api/product/productProperty').subscribe(result => {
      this.properties = result.filter(t=>t.isActive ===true);
    }, error => console.error(error));
  }
  getUnits() {
    this.http.get<Array<Unit>>(this.baseUrl + 'api/unit').subscribe(result => {
      this.units = result.filter(t=>t.isActive ===true);
    }, error => console.error(error));
  }
  OpenModal(content,id:number){  
    this.productAttribute = new ProductAttribute();   
    if(id>0)
    {
      this.http.get<ProductAttribute>(this.baseUrl + 'api/product/productAttributeById/'+id).subscribe(result => {
        this.productAttribute = result;
        this.BindProductAttribute(result);
      }, error => console.error(error));
    }
    else{
       this.ClearForm();
    }
      this.modalReference=this.modalService.open(content);
  }
  BindProductAttribute(data:ProductAttribute) {
    console.log(data);
    this.productAttribute=data;
    this.productAttributeForm.setValue({
      productId : data.productId,
      propertyId:  data.propertyId,
      propertyValue:  data.propertyValue,
      unitValue:  data.unitValue,
      mrp: data.mrp,
      rate: data.rate,
      status:data.isActive
    });
  }

  ClearForm() {
    this.btnSubmited = false;
    this.productAttributeForm.reset();
  }
  SaveProductAttribute(){
    this.btnSubmited = true;
    if (this.productAttributeForm.valid) {   
      this.btnSubmited = false;    
      this.productAttribute.productId=this.productAttributeForm.get('productId').value; 
      this.productAttribute.propertyId=this.productAttributeForm.get('propertyId').value;
      this.productAttribute.propertyValue=this.productAttributeForm.get('propertyValue').value;  
      this.productAttribute.unitValue=parseFloat(this.productAttributeForm.get('unitValue').value);  
      this.productAttribute.mrp=parseFloat(this.productAttributeForm.get('mrp').value);  
      this.productAttribute.rate=parseFloat(this.productAttributeForm.get('rate').value);  
      this.productAttribute.isActive=this.productAttributeForm.get('status').value; 

      this.productAttribute.createdBy=1;
      console.log(this.productAttribute);
      
      this.http.post(this.baseUrl + 'api/Product/ProductAttribute', this.productAttribute).subscribe(
          (response) => {
            this.modalReference.close();
            this.getProductAttributes();
          },
          (error) => console.log(error)        
        )
    } 
    
  }
  

}
