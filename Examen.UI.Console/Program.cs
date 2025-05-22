using System;
using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Services;
using Examen.Infrastructure;

Console.WriteLine("Hello, World!");

EXContext ctx = new EXContext();
UnitOfWork uow = new UnitOfWork(ctx);

LaboratoireService ls = new LaboratoireService(uow);
InfirmierService isvc = new InfirmierService(uow);
PatientService ps = new PatientService(uow);
BilanService bs = new BilanService(uow);

Laboratoire lab1 = new Laboratoire();
lab1.Intitule = "Laboratoire Central";
lab1.Localisation = "Rue 1";
ls.Add(lab1);
ls.Commit();  

Infirmier inf = new Infirmier();
inf.NomComplet = "Infirmier 1";
inf.specialite = Infirmier.Specialite.Hematologie;
inf.LaboratoireFK = lab1.LaboratoireId; 
isvc.Add(inf);
isvc.Commit();  

Patient patient = new Patient();
patient.CodePatient = "P1234";
patient.NomComplet = "Marie Curie";
patient.EmailPatient = "marie.curie@example.com";
patient.NumeroTel = "0123456789";
patient.Informations = "Patient avec allergies";
ps.Add(patient);
ps.Commit();  

Bilan bilan = new Bilan();
bilan.DatePrelevement = DateTime.Now;
bilan.EmailMedecin = "medecin@example.com";
bilan.Paye = false;
bilan.InfirmierFk = inf.InfirmierId;
bilan.PatientFk = patient.CodePatient;
bilan.Infirmier = inf;
bilan.Patient = patient;
bs.Add(bilan);
bs.Commit();
