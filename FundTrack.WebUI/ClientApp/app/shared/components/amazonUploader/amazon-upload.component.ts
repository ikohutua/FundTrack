import { Component } from '@angular/core';
require('aws-sdk/dist/aws-sdk');

@Component({
})
export class AmazonUploadComponent {
    private readonly _amazonKeyId: string = 'AKIAJH4TLLOIZ5QOBKMQ';
    private readonly _amazonKeySecret: string = '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';
    private readonly _amazonBucket: string = 'fundtrack';

/**
 * Function that uploads file to Amazon Web Storage using above settings
   and returns a thenable promise with a data object
   that has Location property that equals the file URL.
 * @param file: file to be uploaded
 * @param fileName: file name including its extension
 */
    public UploadImageToAmazon(file: any, fileName: string): any {
        //let AWSService = window.AWS;
        //AWSService.config.accessKeyId = this._amazonKeyId;
        //AWSService.config.secretAccessKey = this._amazonKeySecret;
        //var upload = new AWSService.S3.ManagedUpload({
        //    params: { Bucket: this._amazonBucket, Key: fileName, Body: file }
        //});
        //var promise = upload.promise();
        //return promise;
    }
}










