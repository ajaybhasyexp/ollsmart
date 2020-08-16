import { Component, OnInit,Inject,ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { NgbModal,NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { DatePipe } from '@angular/common';
import { Expense } from '../../models/Expense';
import { ExpenseHead } from '../../models/ExpenseHead';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import Swal from 'sweetalert2';


import { ExporterService } from '../../services/exporter.service';
@Component({
  selector: 'app-expense-entry',
  templateUrl: './expense-entry.component.html',
  styleUrls: ['./expense-entry.component.css']
})
export class ExpenseEntryComponent implements OnInit {
  displayedColumns: string[] = ['Date','ExpenseHead','Amount','Remarks','Actions'];
  dataSource: MatTableDataSource<Expense>;
  expense = new Expense();
  expenses: Array<Expense> = new Array<Expense>();
  expenseHeads: Array<ExpenseHead> = new Array<ExpenseHead>();
  modalReference: NgbModalRef; 

  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;
  public btnSubmited = false;
  public btnExpSubmited=false;
  
  items  = [
    {id: 1, name: 'Python'},
    {id: 2, name: 'Node Js'},
    {id: 3, name: 'Java'},
    {id: 4, name: 'PHP', disabled: true},
    {id: 5, name: 'Django'},
    {id: 6, name: 'Angular'},
    {id: 7, name: 'Vue'},
    {id: 8, name: 'ReactJs'},
  ];
  selected = [
    {id: 2, name: 'Node Js'},
    {id: 8, name: 'ReactJs'}
  ];
  
  dateRangeForm = new FormGroup({
    fromDateControl: new FormControl(new Date(), Validators.required), 
    toDateControl: new FormControl(new Date(), Validators.required)
  });
  expenseForm = new FormGroup({
    expenseHeadId: new FormControl(null, Validators.required), 
    transDate: new FormControl(new Date(), Validators.required),
    amount: new FormControl('', Validators.required),
    remarks: new FormControl('', Validators.required)
  });
  baseUrl: string;
  constructor(  private http: HttpClient, 
    @Inject('BASE_URL') url: string,
   public dialog: MatDialog,
   private exporterService: ExporterService,
   private modalService: NgbModal,
   private datePipe: DatePipe) { 
      this.baseUrl=url;
 }
 applyFilter(event: Event) {
  const filterValue = (event.target as HTMLInputElement).value;
  this.dataSource.filter = filterValue.trim().toLowerCase();

  if (this.dataSource.paginator) {
    this.dataSource.paginator.firstPage();
  }
}
 exportToExcel() {
  var filtered = this.expenses.map(function(x) { return {'Date':x.transDate,'Expense Head':x.expenseHeadName, 'Amount':x.amount,'Remarks':x.remarks}; })
  this.exporterService.exportJsonToExcel(filtered, 'Expense.xlsx',[]);
 }
  ngOnInit() {
   this.onSubmit();
  }
  getTotalAmount() {
    return this.expenses.map(t => t.amount).reduce((acc, value) => acc + value, 0);
  }
  getExpenseHeads(){ 
    this.http.get<Array<ExpenseHead>>(this.baseUrl + 'api/Expense/ExpenseHeads').subscribe(result => {
      this.expenseHeads =  result.filter(t=>t.isActive ===true);
    }, error => console.error(error));
  }
  onSubmit(){
    this.btnSubmited = true;
    if (this.dateRangeForm.valid) {
      var fromDate=this.datePipe.transform(this.dateRangeForm.get('fromDateControl').value,"yyyy-MM-dd");
      var toDate=this.datePipe.transform(this.dateRangeForm.get('toDateControl').value,"yyyy-MM-dd");
      this.http.get<Array<Expense>>(this.baseUrl + 'api/Expense/ExpenseDetails/'+fromDate+'/'+toDate+'').subscribe(result => {
        this.expenses = result;
        this.dataSource = new MatTableDataSource(this.expenses);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      }, error => console.error(error)); 
    }   
  }
  openModal(content,id:number){
    this.expense = new Expense();   
    this.getExpenseHeads();
    if(id>0)
    {
        this.http.get<Expense>(this.baseUrl + 'api/Expense/ExpenseById/'+id).subscribe(result => {
        this.bindExpense(result);
      }, error => console.error(error));
    }
    else{
      this.clearForm();
    }
    this.modalReference=this.modalService.open(content);
  }
  bindExpense(data:Expense) {
    this.expense=data;
    this.expenseForm.setValue({
      expenseHeadId: data.expenseHeadId,
      remarks: data.remarks,
      amount:data.amount,
      transDate:data.transDate 
    });
  }
  clearForm() {
    this.btnExpSubmited = false;
    this.expenseForm.reset();
  }
  saveExpense(){
    this.btnExpSubmited = true;
    if (this.expenseForm.valid) {   
      this.btnSubmited = false;    
      this.expense.expenseHeadId=this.expenseForm.get('expenseHeadId').value; 
      this.expense.remarks=this.expenseForm.get('remarks').value; 
      this.expense.amount=parseFloat(this.expenseForm.get('amount').value); 
      this.expense.transDate=this.datePipe.transform(this.expenseForm.get('transDate').value,"yyyy-MM-dd");
      this.expense.createdBy=1;
      console.log(this.expense);
        this.http.post(this.baseUrl + 'api/Expense/Expense', this.expense).subscribe(
          (response) => {
            this.modalReference.close();
            this.onSubmit(); 
          },
          (error) => console.log(error)        
        )
    }   
  }
  deleteDialog(data){
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
        this.http.post(this.baseUrl + 'api/Expense/DeleteExpense', data).subscribe(
          (response) => {this.onSubmit(); },
          (error) => console.log(error)        
        )
       
      }
    })
  }
}
