import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { MastersComponent } from './masters/masters.component';
import { BrandsComponent } from './masters/brands/brands.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSliderModule } from '@angular/material/slider';
import {MatDialogModule} from '@angular/material/dialog';
import {DialogContentExampleDialog} from './masters/brands/brands.component';
import {
  MatButtonModule,
  MatFormFieldModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatSelectModule,
  MatSidenavModule,
  MatCardModule,
  MatTableModule,
  MatPaginatorModule
} from "@angular/material";
import { CategoriesComponent } from './masters/categories/categories.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    MastersComponent,
    BrandsComponent,
    CategoriesComponent,
    DialogContentExampleDialog
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    MatSliderModule,
    FormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatListModule,
    MatInputModule,
    MatSelectModule,
    MatSidenavModule,
    MatCardModule,
    MatTableModule,
    MatPaginatorModule,
    MatDialogModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'masters', component: MastersComponent },
      
    ]),
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [DialogContentExampleDialog]
})
export class AppModule { }
