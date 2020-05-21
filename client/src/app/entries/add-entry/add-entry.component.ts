import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
    selector: 'app-add-entry',
    templateUrl: './add-entry.component.html',
    styleUrls: ['./add-entry.component.css']
})
export class AddEntryComponent {

    newEntryForm = this.fb.group({
        description: null,
        value: [null, Validators.required],
        date: [null, Validators.required],
        bankAccountId: [null, Validators.required],
        address2: null,
        city: [null, Validators.required],
        state: [null, Validators.required],
        postalCode: [null, Validators.compose([
            Validators.required, Validators.minLength(5), Validators.maxLength(5)])
        ],
        isFinished: ['true', Validators.required]
    });

    states = [
        { name: 'Alabama', abbreviation: 'AL' },
        { name: 'Alaska', abbreviation: 'AK' },
        { name: 'American Samoa', abbreviation: 'AS' },
        { name: 'Arizona', abbreviation: 'AZ' },
    ];

    constructor(private fb: FormBuilder) { }

    onSubmit() {
        alert('Thanks!');
    }
}
