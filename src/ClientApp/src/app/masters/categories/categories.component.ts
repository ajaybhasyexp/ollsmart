import { Component, OnInit,Inject,ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Category } from '../../models/category';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { NgbModal,NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  displayedColumns: string[] = ['Category','Description','Parent Category','Status','Actions'];
  dataSource: MatTableDataSource<Category>;
  category = new Category();
  categories: Array<Category> = new Array<Category>();
  parentCategories: Array<Category> = new Array<Category>();

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  public btnSubmited = false;
  categoryForm = new FormGroup({
    categoryName: new FormControl('', Validators.required), 
    categoryDescription: new FormControl('', Validators.required),
    parentCategoryId: new FormControl('') 
  });
  baseUrl: string;
  modalReference: NgbModalRef;
  constructor(private http: HttpClient, 
    @Inject('BASE_URL') url: string,
   public dialog: MatDialog,
   private modalService: NgbModal) { 
    this.baseUrl=url;
 }
 
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  ngOnInit() {
    this.getCategories();
    this.getParentCategories(); 
  }
  getCategories() {
    this.http.get<Array<Category>>(this.baseUrl + 'api/category').subscribe(result => {
      this.categories = result;
      this.dataSource = new MatTableDataSource(this.categories);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }, error => console.error(error));
  }
  getParentCategories(){
    this.http.get<Array<Category>>(this.baseUrl + 'api/Category/ParentCategory').subscribe(result => {
      this.parentCategories = result;     
    }, error => console.error(error));
  }
  
  OpenModal(content,id:number){
    this.category = new Category();   
    if(id>0)
    {
        this.http.get<Category>(this.baseUrl + 'api/Category/CategoryById/'+id).subscribe(result => {
        this.BindCategory(result);
        this.category=result;
      }, error => console.error(error));
    }
    else{
      this.clearForm();
    }
    this.modalReference=this.modalService.open(content);
  }
  BindCategory(data:Category) {
    this.category=data;
    console.log(data.parentCategoryId);
    
    this.categoryForm.setValue({
      categoryName: data.categoryName,
      categoryDescription: data.description,
      parentCategoryId: data.parentCategoryId
    });
  }
  clearForm() {
    this.btnSubmited = false;
    this.categoryForm.reset();
   
  }
  saveCategoryDetails(){
    this.btnSubmited = true;
    if (this.categoryForm.valid) {   
      this.btnSubmited = false;    
      this.category.categoryName=this.categoryForm.get('categoryName').value; 
      this.category.description=this.categoryForm.get('categoryDescription').value; 
      this.category.parentCategoryId= this.categoryForm.get('parentCategoryId').value;
      this.category.isActive=true;
      this.category.createdBy=1;
      
      console.log(this.category); 
        this.http.post(this.baseUrl + 'api/Category', this.category).subscribe(
          (response) => console.log(  response),
          (error) => console.log(error)        
        )
        this.modalReference.close();

    } 

    
  }
  

}
