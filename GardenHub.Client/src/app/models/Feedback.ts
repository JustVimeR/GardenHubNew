import { Project } from "./Project";

export interface Feedback {
    project: Project;
    publicationDate: string;
    editedDate: string;
    text: string;
    gardener: string;
  }