import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
    selector: 'app-add-new-entry-dialog',
    templateUrl: './add-new-entry-dialog.component.html',
    styleUrls: ['./add-new-entry-dialog.component.css']
})
export class AddNewEntryDialogComponent {

    constructor(
        public dialogRef: MatDialogRef<AddNewEntryDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

    onNoClick(): void {
        this.dialogRef.close();
    }
}

export interface DialogData {
    animal: string;
    name: string;
}