import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Publisher } from 'src/app/_models/publisher';
import { PublisherService } from 'src/app/_services/publisher.service';

@Component({
  selector: 'app-publisher-add',
  templateUrl: './publisher-add.component.html',
  styleUrls: ['./publisher-add.component.css']
})
export class PublisherAddComponent {
  isNameValid: boolean = true;

  publisher: Publisher = {
    id: 0,
    name: '',
    createdAt: new Date(),
    isDeleted: false,
  };

  constructor(private publisherService: PublisherService, 
    private router: Router, 
    private toastr: ToastrService) {}

  onSubmit() {
    this.publisherService.addPublisher(this.publisher, '1').subscribe(
      (newPublisher) => {
        this.toastr.success('Publisher added successfully', 'Success', {
          timeOut: 5000,
          closeButton: true,
          positionClass: 'toast-top-right',
        });

        this.router.navigate(['/publishers']);
      },
      (error) => {
        const errorMessage = error?.error?.message || 'An error occurred while adding the publisher.';

        this.toastr.error(`Error adding publisher: ${errorMessage}`, 'Error', {
          timeOut: 5000,
          closeButton: true,
          positionClass: 'toast-top-right',
        });
      }
    );
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
