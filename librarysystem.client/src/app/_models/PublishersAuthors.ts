import { Author } from "./author";
import { Publisher } from "./publisher";

export interface PublishersAuthors {
    publishers: Publisher[];
    authors: Author[];
}