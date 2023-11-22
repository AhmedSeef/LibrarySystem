import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Publisher } from 'src/app/_models/publisher';
import { PublisherService } from 'src/app/_services/publisher.service';

@Component({
  selector: 'app-publishers-list',
  templateUrl: './publishers-list.component.html',
  styleUrls: ['./publishers-list.component.css']
})
export class PublishersListComponent implements OnInit {
  publishers: Publisher[] = [];
  includeDeleted: boolean = false;

  constructor(private publisherService: PublisherService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadPublishers();
  }

  openEditModal(publisher: Publisher) {
    const publisherId = publisher.id;
    this.router.navigate(['/publishers', publisherId, 'edit']);
  }

  toggleIncludeDeleted() {
    this.includeDeleted = !this.includeDeleted;
    this.loadPublishers();
  }

  loadPublishers() {
    this.publisherService.getPublishers(this.includeDeleted, '1').subscribe(publishers => {
      this.publishers = publishers;
    });
  }

  openDeleteModal(publisher: Publisher) {
    this.publisherService.deletePublisher(publisher.id, '1').subscribe(
      () => {
        this.toastr.success('Publisher deleted successfully', 'Success', {
          timeOut: 5000,
          closeButton: true,
          positionClass: 'toast-top-right',
        });

        this.loadPublishers();
      },
      (error) => {
        this.toastr.error(`Error deleting publisher: ${error.message}`, 'Error', {
          timeOut: 5000,
          closeButton: true,
          positionClass: 'toast-top-right',
        });

        console.error('Error deleting publisher:', error);
      }
    );
  }

  openViewModal(publisher: Publisher) {
    this.router.navigate(['/publishers', publisher.id]);
  }

  redirectToPublisherForm() {
    this.router.navigate(['/publishers/add']);
  }
}
