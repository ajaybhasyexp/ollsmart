import { Component, OnInit,Inject,ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Unit } from '../../models/Unit';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { NgbModal,NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-units',
  templateUrl: './units.component.html',
  styleUrls: ['./units.component.css']
})
export class UnitsComponent implements OnInit {
  
  displayedColumns: string[] = ['Unit','Description','Status','Actions'];
  dataSource: MatTableDataSource<Unit>;
  unit = new Unit();
  units: Array<Unit> = new Array<Unit>();
  
  public btnSubmited = false;
  
  @ViewChild(MatPaginator, {static: true}) paginator: MatPaginator;
  @ViewChild(MatSort, {static: true}) sort: MatSort;

  unitForm = new FormGroup({
    unitName: new FormControl('', Validators.required), 
    unitDescription: new FormControl('', Validators.required)
  });
  baseUrl: string;
  constructor(  private http: HttpClient, 
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
    this.getUnits(); 
  }
  getUnits() {
    this.http.get<Array<Unit>>(this.baseUrl + 'api/unit').subscribe(result => {
      this.units = result;
      this.dataSource = new MatTableDataSource(this.units);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
      console.log(this.units);
    }, error => console.error(error));
  }
  OpenModal(content,id:number){
    this.unit = new Unit();   
    if(id>0)
    {
        this.http.get<Unit>(this.baseUrl + 'api/Unit/UnitById/'+id).subscribe(result => {
        // result.push(this.unit);
        this.BindUnit(result);
        this.unit=result;
      }, error => console.error(error));
    }
    else{
      this.ClearForm();
    }
    this.modalService.open(content);
  }
  BindUnit(data:Unit) {
    console.log(data);
    this.unit=data;
    this.unitForm.setValue({
      unitName: data.unitName,
      unitDescription: data.description
    });
  }
  ClearForm() {
    this.btnSubmited = false;
    this.unitForm.reset();
  }
  SaveUnitDetails(){
    this.btnSubmited = true;
    if (this.unitForm.valid) {   
      this.btnSubmited = false;    
      this.unit.unitName=this.unitForm.get('unitName').value; 
      this.unit.description=this.unitForm.get('unitDescription').value; 
      this.unit.isActive=true;
      this.unit.createdBy=1;
        this.http.post(this.baseUrl + 'api/Unit', this.unit).subscribe(
          (response) => console.log(  response),
          (error) => console.log(error)        
        )
     
      this.getUnits(); 
    } 
    
  }
  

}
