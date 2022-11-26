﻿using FinalProject.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Core.Service
{
    public interface IDocumentService
    {
        List<Document> GetAlldocument();
        void CREATEdocument(Document document);
        void UPDATEdocument(int id, Document document);
        Document GetdocumentById(int id);
        void Deletedocument(int id);
    }
}
