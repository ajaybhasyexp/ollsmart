import { Component, OnInit,Inject,ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { NgbModal,NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import Swal from 'sweetalert2';


import { ProductProperty } from '../../models/ProductProperty';
import { Unit } from '../../models/Unit';
import { Product } from '../../models/Product';
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

  productAttributeForm= new FormGroup({
    productId: new FormControl(null, Validators.required), 
    propertyId: new FormControl(null, Validators.required),
    propertyValue: new FormControl('', Validators.required),
    unitId: new FormControl(null, Validators.required),
    mrp: new FormControl('', Validators.required),
    rate: new FormControl('', Validators.required),
    status: new FormControl(null, Validators.required),
  });
  displayedColumns: string[] = ['Product','Description','Category','Brand','Status','Actions'];
  // dataSource: MatTableDataSource<Product>;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  constructor(private http: HttpClient, 
    @Inject('BASE_URL') url: string,
   public dialog: MatDialog,
   private modalService: NgbModal) { 
    this.baseUrl=url;
 }

  ngOnInit() {
    this.getProducts();
    this.getProductProperties();
    this.getUnits();
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
    if(id>0)
    {
      // this.http.get<Product>(this.baseUrl + 'api/product/productsById/'+id).subscribe(result => {
      //   this.product = result;
      //   this.BindProduct(result);
      // }, error => console.error(error));
    }
    else{
      // this.ClearForm();
    }
      this.modalReference=this.modalService.open(content);
  }

}
