import { Component, OnInit,Inject,ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ProductProperty } from '../../models/ProductProperty';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { NgbModal ,NgbModalRef} from '@ng-bootstrap/ng-bootstrap';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-product-property',
  templateUrl: './product-property.component.html',
  styleUrls: ['./product-property.component.css']
})
export class ProductPropertyComponent implements OnInit {
  displayedColumns: string[] = ['Property','Status','Actions'];
  dataSource: MatTableDataSource<ProductProperty>;
  productProperty = new ProductProperty();
  productProperties: Array<ProductProperty> = new Array<ProductProperty>();
  modalReference: NgbModalRef;
  public btnSubmited = false;
  
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  productPropertyForm = new FormGroup({
   productPropertyName: new FormControl('', Validators.required)
  });
  baseUrl: string;
  constructor(private http: HttpClient, 
     @Inject('BASE_URL') url: string,
    public dialog: MatDialog,
    private modalService: NgbModal) { 
     this.baseUrl=url;
  }

  ngOnInit() {
    this.getProductProperties(); 
  }
  getProductProperties() {
    this.http.get<Array<ProductProperty>>(this.baseUrl + 'api/product/productProperty').subscribe(result => {
      this.productProperties = result;
      this.dataSource = new MatTableDataSource(this.productProperties);
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
  
  OpenModal(content,id:number){
    this.productProperty = new ProductProperty();   
    if(id>0)
    {
        this.http.get<ProductProperty>(this.baseUrl + 'api/Product/ProductPropertyById/'+id).subscribe(result => {
        this.BindProductProperty(result);
        this.productProperty=result;
      }, error => console.error(error));
    }
    else{
      this.ClearForm();
    }
    this.modalReference=this.modalService.open(content);
  }
  BindProductProperty(data:ProductProperty) {
    console.log(data);
    this.productProperty=data;
    this.productPropertyForm.setValue({
      productPropertyName: data.propertyName,
    });
  }

  SaveProductPropertyDetails (){
    this.btnSubmited = true;
    if (this.productPropertyForm.valid) {   
      this.btnSubmited = false;    
      this.productProperty.propertyName=this.productPropertyForm.get('productPropertyName').value; 
      this.productProperty.isActive=true;
      this.productProperty.createdBy=1;
      console.log(this.productProperty);
      this.http.post(this.baseUrl + 'api/product/productProperty', this.productProperty).subscribe(
        (response) => {
          console.log( response);
          this.modalReference.close();
          this.getProductProperties(); 
        },
          (error) => console.log(error)        
        )
    }  
  }
  ClearForm() {
    this.btnSubmited = false;
    this.productPropertyForm.reset();
   
  }

}
