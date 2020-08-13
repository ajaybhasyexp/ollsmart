import { Component, OnInit,Inject,ViewChild,ElementRef } from '@angular/core';
import * as xlsx from 'xlsx';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ExpenseHead } from '../../models/ExpenseHead';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { NgbModal,NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

import { ExporterService } from '../../services/exporter.service';

@Component({
  selector: 'app-expense-heads',
  templateUrl: './expense-heads.component.html',
  styleUrls: ['./expense-heads.component.css']
})
export class ExpenseHeadsComponent implements OnInit {
  displayedColumns: string[] = ['ExpenseHead','Description','Status','Actions'];
  dataSource: MatTableDataSource<ExpenseHead>;
  expenseHead = new ExpenseHead();
  expenseHeads: Array<ExpenseHead> = new Array<ExpenseHead>();
  modalReference: NgbModalRef; 

  public btnSubmited = false;
  
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  @ViewChild('epltable', { static: false }) epltable: ElementRef;

  expenseHeadForm = new FormGroup({
    expenseHeadName: new FormControl('', Validators.required), 
    description: new FormControl('', Validators.required), 
    status: new FormControl('', Validators.required)
  });
  baseUrl: string;
  constructor(  private http: HttpClient, 
     @Inject('BASE_URL') url: string,
    public dialog: MatDialog,
    private exporterService: ExporterService,
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
    this.getExpenseHeads();
  }
  exportToExcel() {
    var filtered = this.expenseHeads.map(function(x) { return {'Expense Head':x.expenseHeadName, 'Description':x.description,'Status':x.isActive}; })
    this.exporterService.exportJsonToExcel(filtered, 'ExpenseHeads.xlsx',[]);
   }
  getExpenseHeads(){ 
    this.http.get<Array<ExpenseHead>>(this.baseUrl + 'api/Expense/ExpenseHeads').subscribe(result => {
      this.expenseHeads = result;
      this.dataSource = new MatTableDataSource(this.expenseHeads);
      this.dataSource.paginator = this.paginator; 
      this.dataSource.sort = this.sort;
    }, error => console.error(error));
  }
  OpenModal(content,id:number){
    this.expenseHead = new ExpenseHead();   
    if(id>0)
    {
        this.http.get<ExpenseHead>(this.baseUrl + 'api/Expense/ExpenseHeadById/'+id).subscribe(result => {
        this.BindExpenseHead(result);
        this.expenseHead=result;
      }, error => console.error(error));
    }
    else{
      this.ClearForm();
    }
    this.modalReference=this.modalService.open(content);
  }
  BindExpenseHead(data:ExpenseHead) {
    console.log(data);
    this.expenseHead=data;
    this.expenseHeadForm.setValue({
      expenseHeadName: data.expenseHeadName,
      description: data.description,
      status:data.isActive
    });
  }
  ClearForm() {
    this.btnSubmited = false;
    this.expenseHeadForm.reset();
  }
  SaveExpenseHead(){
    this.btnSubmited = true;
    if (this.expenseHeadForm.valid) {   
      this.btnSubmited = false;    
      this.expenseHead.expenseHeadName=this.expenseHeadForm.get('expenseHeadName').value; 
      this.expenseHead.description=this.expenseHeadForm.get('description').value; 
      this.expenseHead.isActive=this.expenseHeadForm.get('status').value; 

      this.expenseHead.createdBy=1;
        this.http.post(this.baseUrl + 'api/Expense/ExpenseHead', this.expenseHead).subscribe(
          (response) => {
            this.modalReference.close();
            this.getExpenseHeads(); 
          },
          (error) => console.log(error)        
        )
    
    } 
    
  }
  

}
