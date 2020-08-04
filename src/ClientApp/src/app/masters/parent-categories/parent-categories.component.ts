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
  selector: 'app-parent-categories',
  templateUrl: './parent-categories.component.html',
  styleUrls: ['./parent-categories.component.css']
})
export class ParentCategoriesComponent implements OnInit {
  displayedColumns: string[] = ['Category','Description','Status','Actions'];
  dataSource: MatTableDataSource<Category>;
  category = new Category();
  categories: Array<Category> = new Array<Category>();
  
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  public btnSubmited = false;
  parentCategoryForm= new FormGroup({
    categoryName: new FormControl('', Validators.required), 
    categoryDescription: new FormControl('', Validators.required),
    status:new FormControl('', Validators.required),
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
    this.getParentCategories(); 
  }
  getParentCategories() {
    this.http.get<Array<Category>>(this.baseUrl + 'api/category/ParentCategory').subscribe(result => {
      this.categories = result;
      this.dataSource = new MatTableDataSource(this.categories);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    }, error => console.error(error));
  }

  
  OpenModal(content,id:number){
    this.category = new Category();   
    if(id>0)
    {
        this.http.get<Category>(this.baseUrl + 'api/Category/CategoryById/'+id).subscribe(result => {
        this.BindParentCategory(result);
        this.category=result;
      }, error => console.error(error));
    }
    else{
      this.clearForm();
    }
    this.modalReference=this.modalService.open(content);
  }
  BindParentCategory(data:Category) { 
    this.category=data;
 
    this.parentCategoryForm.setValue({
      categoryName: data.categoryName,
      categoryDescription: data.description,
      status:data.isActive
    });
  }
  clearForm() {
    this.btnSubmited = false;
    this.parentCategoryForm.reset();
   
  }
  saveCategoryDetails(){
    this.btnSubmited = true;
    if (this.parentCategoryForm.valid) {   
      this.btnSubmited = false;    
      this.category.categoryName=this.parentCategoryForm.get('categoryName').value; 
      this.category.description=this.parentCategoryForm.get('categoryDescription').value; 
      this.category.isActive= this.parentCategoryForm.get('status').value;
      this.category.createdBy=1;
      
      this.http.post(this.baseUrl + 'api/Category', this.category).subscribe(
          (response) => {
            console.log( response);
            this.modalReference.close();
            this.getParentCategories(); 
          },
          (error) => console.log(error)        
        )

    } 

    
  }
  

}
