import { Component, OnInit,Inject,ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Brand } from '../../models/brand';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';


export interface DialogData {
  animal: 'panda' | 'unicorn' | 'lion';
}
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
  
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  brandForm = new FormGroup({
    brandName: new FormControl('', Validators.required),
    brandDescription: new FormControl('', Validators.required),
    brandStatus: new FormControl('', Validators.required),
  });
  constructor(http: HttpClient, 
    @Inject('BASE_URL') baseUrl: string,
    public dialog: MatDialog) { 
    
    http.get<Array<Brand>>(baseUrl + 'api/brand').subscribe(result => {
      this.brands = result;
      this.dataSource = new MatTableDataSource(this.brands);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      console.log(this.brands);
    }, error => console.error(error));
  }

  ngOnInit() {
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  OpenEditModal(){
    const dialogRef = this.dialog.open(DialogContentExampleDialog);

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }
  OpenAddModal(){
    const dialogRef = this.dialog.open(DialogContentExampleDialog,
      {
        data: {
          animal: 'panda'
        }
      });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }
  OpenDeleteModal(content) {
   // this.modalReference = this.modalService.open(content);
  }
  
  

}
@Component({
  selector: 'edit-brand-dialog',
  templateUrl: 'edit-brand-dialog.html',
})
export class DialogContentExampleDialog {
  brandForm = new FormGroup({
    brandName: new FormControl('', Validators.required),
    brandDescription: new FormControl('', Validators.required),
    brandStatus: new FormControl('', Validators.required),
  });
  constructor(
    public dialogRef: MatDialogRef<DialogContentExampleDialog>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {}

  onNoClick(): void {
    this.dialogRef.close();
  }
  saveBrandDetails(){
    if (this.brandForm.valid) {         
      console.log(this.brandForm);
    }
    else {
     alert('Please enter all details');
    }
  }
  
  
}