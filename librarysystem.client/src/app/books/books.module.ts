// books/books.module.ts

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BooksRoutingModule } from './books-routing.module';
import { BooksListComponent } from './books-list/books-list.component';
import { BookDetailsComponent } from './book-details/book-details.component';
import { BookEditComponent } from './book-edit/book-edit.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BooksAddComponent } from './books-add/books-add.component';

@NgModule({
  declarations: [
    BooksListComponent,
    BookDetailsComponent,
    BookEditComponent,
    BooksAddComponent,
  ],
  imports: [
    CommonModule,
    BooksRoutingModule,
    HttpClientModule,
    FormsModule,
  ]
})
export class BooksModule { }

