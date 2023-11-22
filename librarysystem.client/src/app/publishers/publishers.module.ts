// publishers/publishers.module.ts

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PublishersRoutingModule } from './publishers-routing.module';
import { PublishersListComponent } from './publishers-list/publishers-list.component';
import { PublisherDetailsComponent } from './publisher-details/publisher-details.component';
import { PublisherEditComponent } from './publisher-edit/publisher-edit.component';
import { PublisherAddComponent } from './publisher-add/publisher-add.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    PublishersListComponent,
    PublisherDetailsComponent,
    PublisherEditComponent,
    PublisherAddComponent
  ],
  imports: [
    CommonModule,
    PublishersRoutingModule,
    FormsModule
  ]
})
export class PublishersModule { }
