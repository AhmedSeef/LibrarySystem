import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AuthorsModule } from './authors/authors.module';
import { BooksModule } from './books/books.module';
import { PublishersModule } from './publishers/publishers.module';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';



const appRoutes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'authors' },
  { path: 'authors', loadChildren: () => import('./authors/authors.module').then((m) => m.AuthorsModule) },
  { path: 'books', loadChildren: () => import('./books/books.module').then((m) => m.BooksModule) },
  { path: 'publishers', loadChildren: () => import('./publishers/publishers.module').then((m) => m.PublishersModule) }
];

@NgModule({
  declarations: [
    AppComponent,
    NavComponent
  ],
  imports: [
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-top-right',
      closeButton: true,
    }),
    ReactiveFormsModule,
    AuthorsModule,
    BooksModule,
    PublishersModule,
    BrowserModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
