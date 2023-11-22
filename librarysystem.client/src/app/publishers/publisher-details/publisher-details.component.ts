import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Publisher } from 'src/app/_models/publisher';
import { PublisherService } from 'src/app/_services/publisher.service';

@Component({
  selector: 'app-publisher-details',
  templateUrl: './publisher-details.component.html',
  styleUrls: ['./publisher-details.component.css']
})
export class PublisherDetailsComponent implements OnInit {
  publisher: Publisher = {
    id: 0,
    name: '',
    createdAt: new Date(),
    isDeleted: false,
  };

  constructor(private publisherService: PublisherService,
     private route: ActivatedRoute, 
     private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadPublisherDetails();
  }

  loadPublisherDetails() {
    const idParam = this.route.snapshot.paramMap.get('id');

    if (idParam !== null) {
      const publisherId = +idParam;

      this.publisherService.getPublisher(publisherId, '1').subscribe(
        (publisher) => {
          this.publisher = publisher;
        },
        (error) => {
          this.toastr.error('Error fetching publisher details', 'Error', {
            timeOut: 5000,
            closeButton: true,
            positionClass: 'toast-top-right',
          });
        }
      );
    }
  }
}
