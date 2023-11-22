import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Publisher } from 'src/app/_models/publisher';
import { PublisherService } from 'src/app/_services/publisher.service';

@Component({
  selector: 'app-publisher-edit',
  templateUrl: './publisher-edit.component.html',
  styleUrls: ['./publisher-edit.component.css']
})
export class PublisherEditComponent implements OnInit {
  publisher: Publisher = {
    id: 0,
    name: '',
    createdAt: new Date(),
    isDeleted: false,
  };
  isNameValid: boolean = true;

  constructor(
    private publisherService: PublisherService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
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

          console.error('Error fetching publisher details:', error);
        }
      );
    }
  }

  onSubmit() {
    if (this.isNameValid) {
      this.publisherService.updatePublisher(this.publisher.id, this.publisher, '1').subscribe(
        (updatedPublisher) => {
          this.toastr.success('Publisher updated successfully', 'Success', {
            timeOut: 5000,
            closeButton: true,
            positionClass: 'toast-top-right',
          });

          this.router.navigate(['/publishers']);
        },
        (error) => {
          this.toastr.error('Error updating publisher', 'Error', {
            timeOut: 5000,
            closeButton: true,
            positionClass: 'toast-top-right',
          });

          console.error('Error updating publisher:', error);
        }
      );
    }
  }

  onNameChange() {
    this.isNameValid = this.validateName();
  }

  validateName(): boolean {
    const name = this.publisher.name;
    const isValid = name.length >= 10 && name.length <= 50;

    if (!isValid) {
      this.toastr.warning('Name must be between 10 and 50 characters', 'Warning', {
        timeOut: 5000,
        closeButton: true,
        positionClass: 'toast-top-right',
      });
    }

    return isValid;
  }
}
