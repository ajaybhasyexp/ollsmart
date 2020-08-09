import { Component, OnInit,Inject,ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { NgbModal,NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import Swal from 'sweetalert2';

import { Category } from '../../models/category';
import { Brand } from '../../models/brand';
import { Unit } from '../../models/unit';
import { Product } from '../../models/Product';


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  modalReference: NgbModalRef;
  btnSubmited :boolean= false;
  baseUrl: string;
  categories: Array<Category> = new Array<Category>();
  brands: Array<Brand> = new Array<Brand>();
  units: Array<Unit> = new Array<Unit>();
  products:Array<Product> = new Array<Product>();
  product = new Product();
  
  displayedColumns: string[] = ['Product','Description','Category','Brand','Status','Actions'];
  dataSource: MatTableDataSource<Product>;
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  productForm= new FormGroup({
    productName: new FormControl('', Validators.required), 
    productDescription: new FormControl('', Validators.required),
    brandId: new FormControl(null, Validators.required),
    categoryId: new FormControl(null, Validators.required),
    status: new FormControl(null, Validators.required),
  });

  constructor(private http: HttpClient, 
    @Inject('BASE_URL') url: string,
   public dialog: MatDialog,
   private modalService: NgbModal) { 
    this.baseUrl=url;
 }
  ngOnInit() {
    this.getProducts();
    this.getCategories();
    this.getBrands();

  }
  clearForm() {
    this.btnSubmited = false;
    this.productForm.reset(); 
   
  }
  getProducts() {
    this.http.get<Array<Product>>(this.baseUrl + 'api/product').subscribe(result => {
      this.products = result;
      this.dataSource = new MatTableDataSource(this.products);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      console.log(this.products);
    }, error => console.error(error));
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  getBrands() {
    this.http.get<Array<Brand>>(this.baseUrl + 'api/brand').subscribe(result => {
      this.brands = result.filter(t=>t.isActive ===true);
    }, error => console.error(error));
  }

  getCategories() {
    this.http.get<Array<Category>>(this.baseUrl + 'api/Category/SubCategory/0').subscribe(result => {
      this.categories = result.filter(t=>t.isActive ===true);
    }, error => console.error(error));
  }
  OpenModal(content,id:number){   
    this.product = new Product();   
    if(id>0)
    {
      this.http.get<Product>(this.baseUrl + 'api/product/productsById/'+id).subscribe(result => {
        this.product = result;
        this.BindProduct(result);
      }, error => console.error(error));
    }
    else{
      this.ClearForm();
    }
      this.modalReference=this.modalService.open(content);
  }
  BindProduct(data){
    this.product=data;
    this.productForm.setValue({
      productName: data.productName,
      productDescription: data.description,
      categoryId: data.categoryId,
      brandId: data.brandId,
      status:data.isActive
    });
  }
  SaveProduct() {
    this.btnSubmited = true;
    if (this.productForm.valid) {   
      this.btnSubmited = false;    
      this.product.productName=this.productForm.get('productName').value; 
      this.product.description=this.productForm.get('productDescription').value; 
      this.product.categoryId= this.productForm.get('categoryId').value;
      this.product.brandId= this.productForm.get('brandId').value;
      this.product.isActive=this.productForm.get('status').value;
      this.product.createdBy=1;
      console.log(this.product);
      this.http.post(this.baseUrl + 'api/product', this.product).subscribe(
          (response) => {
          this.modalReference.close();
          this.getProducts();
        },
          (error) => console.log(error)        
        )
    }  
  }
  ClearForm() {
    this.btnSubmited = false;
    this.productForm.reset();
   
  }
}
