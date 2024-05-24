import { SafeResourceUrl } from "@angular/platform-browser";

export interface Book {
    bookTitle: string,
    bookDescription: string,
    author: string,
    publishDate: string,
    coverBase64: string,
    coverSrc: SafeResourceUrl
}
