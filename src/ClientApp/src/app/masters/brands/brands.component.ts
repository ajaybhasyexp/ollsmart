import { Component, OnInit,Inject,ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Brand } from '../../models/brand';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { NgbModal,NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-brands',
  templateUrl: './brands.component.html',
  styleUrls: ['./brands.component.css']
})
export class BrandsComponent implements OnInit {

  displayedColumns: string[] = ['Brand','Description','Status','Actions'];
  dataSource: MatTableDataSource<Brand>;
  brand = new Brand();
  brands: Array<Brand> = new Array<Brand>();
  modalReference: NgbModalRef;
  
  public btnSubmited = false;
  
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  brandForm = new FormGroup({
    brandName: new FormControl('', Validators.required), 
    brandDescription: new FormControl('', Validators.required)
  });
  baseUrl: string;
  constructor(private http: HttpClient, 
     @Inject('BASE_URL') url: string,
    public dialog: MatDialog,
    private modalService: NgbModal) { 
     this.baseUrl=url;
  }

  ngOnInit() {
    this.getBrands(); 
  }
  getBrands() {
    this.http.get<Array<Brand>>(this.baseUrl + 'api/brand').subscribe(result => {
      this.brands = result;
      this.dataSource = new MatTableDataSource(this.brands);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      console.log(this.brands);
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
    this.brand = new Brand();   
    if(id>0)
    {
        this.http.get<Brand>(this.baseUrl + 'api/Brand/BrandById/'+id).subscribe(result => {
        // result.push(this.brand);
        this.BindBrand(result);
        this.brand=result;
      }, error => console.error(error));
    }
    else{
      this.ClearForm();
    }
    this.modalReference =this.modalService.open(content);
  }
  BindBrand(data:Brand) {
    console.log(data);
    this.brand=data;
    this.brandForm.setValue({
      brandName: data.brandName,
      brandDescription: data.description
    });
  }

  SaveBrandDetails(){
    this.btnSubmited = true;
    if (this.brandForm.valid) {   
      this.btnSubmited = false;    
      this.brand.brandName=this.brandForm.get('brandName').value; 
      this.brand.description=this.brandForm.get('brandDescription').value; 
      this.brand.isActive=true;
      this.brand.createdBy=1;
      this.http.post(this.baseUrl + 'api/Brand', this.brand).subscribe(
        (response) => {
          console.log( response);
          this.modalReference.close();
          this.getBrands(); 
        },
        (error) => console.log(error)        
      )
    }  
  }

  ClearForm() {
    this.btnSubmited = false;
    this.brandForm.reset(); 
  }
  DeleteDialog(data)
  {
    console.log(data);
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.value) {
        console.log('ss');
        this.http.post(this.baseUrl + 'api/Brand/DeleteBrand', data).subscribe(
          (response) =>{this.getBrands(); },
          (error) => console.log(error)        
        )
      }
    })
  }
}
