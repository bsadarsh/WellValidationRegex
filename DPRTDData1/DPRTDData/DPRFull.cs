using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DPRTDValidation.Code;


namespace ValidationObjectsLibrary2011
{
    class DPRFull
    {
        #region Instance Variables
        private int _opStatusId;
        private string _group;
        private string _company;
        private string _businessUnit;
        private string _country;
        private string _formalWellName;
        private string _commonWellName;
        private string _basinName;
        private string _blockNumber;
        private double? _latDeg;
        private double? _latMin;
        private double? _latSec;
        private string _ns;
        private double? _longDeg;
        private double? _longMin;
        private double? _longSec;
        private string _ew;
        private string _drillingContractor;
        private string _rigName;
        private int? _ownerDrilled;
        private string _holeType;
        private string _locatorWell;
        private string _multiLateral;
        private string _multiLateralJunctionType;
        private double? _numberOfLaterals;
        private double? _continuingGeolSTracks;
        private double? _mechanicalSTracks;
        private string _reSpuddedreDrilled;
        private string _originalNameForReSpud;
        private string _wellType;
        private string _playType;
        private string _hp;
        private string _ht;
        private string _rigType;
        private string _drillingMethod;
        private string _unitOfMeasurement;
        private double? _waterDepth;
        private double? _spudDepthBrt;
        private double? _mtd;
        private double? _unusedLengthsDueToStracks;
        private double? _locatorUnusedLengths;
        private double? _tvd;
        private string _subSalt;
        private double? _tvdStartOfSalt;
        private double? _tvdEndOfSalt;
        private string _complexWell;
        private double? _maximumAngleDegrees;
        private double? _totalLengthHorizSections;
        private double? _finalDrillBitSize;
        private string _preCasing1;
        private string _preCasing2;
        private string _preCasing3;
        private string _preCasing4;
        private string _preCasing5;
        private string _preCasing6;
        private string _preCasing7;
        private string _preCasing8;
        private string _preCasing9;
        private string _preCasing10;
        private string _preCasing11;
        private string _newConductorCasing;
        private string _conductorInstalledByRig;
        private string _newCasing2;
        private string _newCasing3;
        private string _newCasing4;
        private string _newCasing5;
        private string _newCasing6;
        private string _newCasing7;
        private string _newCasing8;
        private string _newCasing9;
        private string _newCasing10;
        private string _newCasing11;
        private string _underBalancedWell;
        private string _drillingFluidType;
        private string _mudWeightUnits;
        private double? _mudWeightTd;
        private double? _maxMudWeight;
        private string _cuttingsDisposal;
        private double? _coringDays;
        private double? _coringInterval;
        private double? _loggingDaysNotAtTd;
        private double? _loggingDaysAtTd;
        private double? _underReamingDays;
        private double? _underReamedInterval;
        private string _fewd;
        private string _ageOfDeepestResevoir;
        private string _newTechniques;
        private double? _rigMoveTime;
        private string _rigMoveWithinField;
        private double? _geoSideTrackWhipstockDays;
        private double? _slotRecoveryDays;
        private string _offlineSlotRecoveryOps;
        private string _batchSetDrilled;
        private DateTime? _spudDate;
        private double? _dryHoleDays;
        private DateTime? _endOfDryholePeriod;
        private int? _dryHoleSuspensions;
        private double? _daysSpentSuspensions;
        private double? _totalWellSiteDays;
        private double? _dryHoleCost;
        private string _prelimOrFinalCost;
        private string _completenessOfCost;
        private double? _totalWellCost;
        private string _currency;
        private string _wellStauts;
        private double? _paSuCoDays;
        private double? _otherOPdays;
        private DateTime? _endOfWellOps;
        private double? _wowBeforeMoveOff;
        private string _rigMooringSystem;
        private double? _daysSpentOnMooring;
        private double? _daysSpentDeMooring;
        private double? _wowDuringMooring;
        private double? _wowDuringDeMooring;
        private double? _totalWowDuringDryhole;
        private double? _totalNptExclWow;
        private double? _nptDueToContractor;
        private double? _nptDueToServiceCompany;
        private double? _nptDueToOperatorProbs;
        private double? _nptDueToExternalProbs;
        private double? _nptDueToDownholeProbs;
        private string _mainInterruptsDryHole;
        private string _furtherDetails;
        private string _comments;
        private string _comments2;
        private string _apiWellNumber;
        private string _lwlWellName;
        private double? _lwlLoggingTime;
        private string _dataType = "F";
        private List<ValidationError> _errors = new List<ValidationError>();
        private int _row;
       // private readonly ColumnAttributeMapping _mapping;


    #endregion

        #region Properties
        public int OpStatusId
        {
            get { return _opStatusId; }
            set { _opStatusId = value; }
        }

        public string Group
        {
            get { return _group; }
            set { _group = value; }
        }

        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }

        public string BusinessUnit
        {
            get { return _businessUnit; }
            set { _businessUnit = value; }
        }

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        public string FormalWellName
        {
            get { return _formalWellName; }
            set { _formalWellName = value; }
        }

        public string CommonWellName
        {
            get { return _commonWellName; }
            set { _commonWellName = value; }
        }

        public string BasinName
        {
            get { return _basinName; }
            set { _basinName = value; }
        }

        public string BlockNumber
        {
            get { return _blockNumber; }
            set { _blockNumber = value; }
        }

        public double? LatDeg
        {
            get { return _latDeg; }
            set { _latDeg = value; }
        }

        public double? LatMin
        {
            get { return _latMin; }
            set { _latMin = value; }
        }

        public double? LatSec
        {
            get { return _latSec; }
            set { _latSec = value; }
        }

        public string Ns
        {
            get { return _ns; }
            set { _ns = value; }
        }

        public double? LongDeg
        {
            get { return _longDeg; }
            set { _longDeg = value; }
        }

        public double? LongMin
        {
            get { return _longMin; }
            set { _longMin = value; }
        }

        public double? LongSec
        {
            get { return _longSec; }
            set { _longSec = value; }
        }

        public string Ew
        {
            get { return _ew; }
            set { _ew = value; }
        }

        public string DrillingContractor
        {
            get { return _drillingContractor; }
            set { _drillingContractor = value; }
        }

        public string RigName
        {
            get { return _rigName; }
            set { _rigName = value; }
        }

        public int? OwnerDrilled
        {
            get { return _ownerDrilled; }
            set { _ownerDrilled = value; }
        }

        public string HoleType
        {
            get { return _holeType; }
            set { _holeType = value; }
        }

        public string LocatorWell
        {
            get { return _locatorWell; }
            set { _locatorWell = value; }
        }

        public string MultiLateral
        {
            get { return _multiLateral; }
            set { _multiLateral = value; }
        }

        public string MultiLateralJunctionType
        {
            get { return _multiLateralJunctionType; }
            set { _multiLateralJunctionType = value; }
        }

        public double? NumberOfLaterals
        {
            get { return _numberOfLaterals; }
            set { _numberOfLaterals = value; }
        }

        public double? ContinuingGeolSTracks
        {
            get { return _continuingGeolSTracks; }
            set { _continuingGeolSTracks = value; }
        }

        public double? MechanicalSTracks
        {
            get { return _mechanicalSTracks; }
            set { _mechanicalSTracks = value; }
        }

        public string ReSpuddedreDrilled
        {
            get { return _reSpuddedreDrilled; }
            set { _reSpuddedreDrilled = value; }
        }

        public string OriginalNameForReSpud
        {
            get { return _originalNameForReSpud; }
            set { _originalNameForReSpud = value; }
        }

        public string WellType
        {
            get { return _wellType; }
            set { _wellType = value; }
        }

        public string PlayType
        {
            get { return _playType; }
            set { _playType = value; }
        }

        public string Hp
        {
            get { return _hp; }
            set { _hp = value; }
        }

        public string Ht
        {
            get { return _ht; }
            set { _ht = value; }
        }

        public string RigType
        {
            get { return _rigType; }
            set { _rigType = value; }
        }

        public string DrillingMethod
        {
            get { return _drillingMethod; }
            set { _drillingMethod = value; }
        }

        public string UnitOfMeasurement
        {
            get { return _unitOfMeasurement; }
            set { _unitOfMeasurement = value; }
        }

        public double? WaterDepth
        {
            get { return _waterDepth; }
            set { _waterDepth = value; }
        }

        public double? SpudDepthBrt
        {
            get { return _spudDepthBrt; }
            set { _spudDepthBrt = value; }
        }

        public double? Mtd
        {
            get { return _mtd; }
            set { _mtd = value; }
        }

        public double? UnusedLengthsDueToStracks
        {
            get { return _unusedLengthsDueToStracks; }
            set { _unusedLengthsDueToStracks = value; }
        }

        public double? LocatorUnusedLengths
        {
            get { return _locatorUnusedLengths; }
            set { _locatorUnusedLengths = value; }
        }

        public double? Tvd
        {
            get { return _tvd; }
            set { _tvd = value; }
        }

        public string SubSalt
        {
            get { return _subSalt; }
            set { _subSalt = value; }
        }

        public double? TvdStartOfSalt
        {
            get { return _tvdStartOfSalt; }
            set { _tvdStartOfSalt = value; }
        }

        public double? TvdEndOfSalt
        {
            get { return _tvdEndOfSalt; }
            set { _tvdEndOfSalt = value; }
        }

        public string ComplexWell
        {
            get { return _complexWell; }
            set { _complexWell = value; }
        }

        public double? MaximumAngleDegrees
        {
            get { return _maximumAngleDegrees; }
            set { _maximumAngleDegrees = value; }
        }

        public double? TotalLengthHorizSections
        {
            get { return _totalLengthHorizSections; }
            set { _totalLengthHorizSections = value; }
        }

        public double? FinalDrillBitSize
        {
            get { return _finalDrillBitSize; }
            set { _finalDrillBitSize = value; }
        }

        public string PreCasing1
        {
            get { return _preCasing1; }
            set { _preCasing1 = value; }
        }

        public string PreCasing2
        {
            get { return _preCasing2; }
            set { _preCasing2 = value; }
        }

        public string PreCasing3
        {
            get { return _preCasing3; }
            set { _preCasing3 = value; }
        }

        public string PreCasing4
        {
            get { return _preCasing4; }
            set { _preCasing4 = value; }
        }

        public string PreCasing5
        {
            get { return _preCasing5; }
            set { _preCasing5 = value; }
        }

        public string PreCasing6
        {
            get { return _preCasing6; }
            set { _preCasing6 = value; }
        }

        public string PreCasing7
        {
            get { return _preCasing7; }
            set { _preCasing7 = value; }
        }

        public string PreCasing8
        {
            get { return _preCasing8; }
            set { _preCasing8 = value; }
        }

        public string PreCasing9
        {
            get { return _preCasing9; }
            set { _preCasing9 = value; }
        }

        public string PreCasing10
        {
            get { return _preCasing10; }
            set { _preCasing10 = value; }
        }

        public string PreCasing11
        {
            get { return _preCasing11; }
            set { _preCasing11 = value; }
        }

        public string NewConductorCasing
        {
            get { return _newConductorCasing; }
            set { _newConductorCasing = value; }
        }

        public string ConductorInstalledByRig
        {
            get { return _conductorInstalledByRig; }
            set { _conductorInstalledByRig = value; }
        }

        public string NewCasing2
        {
            get { return _newCasing2; }
            set { _newCasing2 = value; }
        }

        public string NewCasing3
        {
            get { return _newCasing3; }
            set { _newCasing3 = value; }
        }

        public string NewCasing4
        {
            get { return _newCasing4; }
            set { _newCasing4 = value; }
        }

        public string NewCasing5
        {
            get { return _newCasing5; }
            set { _newCasing5 = value; }
        }

        public string NewCasing6
        {
            get { return _newCasing6; }
            set { _newCasing6 = value; }
        }

        public string NewCasing7
        {
            get { return _newCasing7; }
            set { _newCasing7 = value; }
        }

        public string NewCasing8
        {
            get { return _newCasing8; }
            set { _newCasing8 = value; }
        }

        public string NewCasing9
        {
            get { return _newCasing9; }
            set { _newCasing9 = value; }
        }

        public string NewCasing10
        {
            get { return _newCasing10; }
            set { _newCasing10 = value; }
        }

        public string NewCasing11
        {
            get { return _newCasing11; }
            set { _newCasing11 = value; }
        }

        public string UnderBalancedWell
        {
            get { return _underBalancedWell; }
            set { _underBalancedWell = value; }
        }

        public string DrillingFluidType
        {
            get { return _drillingFluidType; }
            set { _drillingFluidType = value; }
        }

        public string MudWeightUnits
        {
            get { return _mudWeightUnits; }
            set { _mudWeightUnits = value; }
        }

        public double? MudWeightTd
        {
            get { return _mudWeightTd; }
            set { _mudWeightTd = value; }
        }

        public double? MaxMudWeight
        {
            get { return _maxMudWeight; }
            set { _maxMudWeight = value; }
        }

        public string CuttingsDisposal
        {
            get { return _cuttingsDisposal; }
            set { _cuttingsDisposal = value; }
        }

        public double? CoringDays
        {
            get { return _coringDays; }
            set { _coringDays = value; }
        }

        public double? CoringInterval
        {
            get { return _coringInterval; }
            set { _coringInterval = value; }
        }

        public double? LoggingDaysNotAtTd
        {
            get { return _loggingDaysNotAtTd; }
            set { _loggingDaysNotAtTd = value; }
        }

        public double? LoggingDaysAtTd
        {
            get { return _loggingDaysAtTd; }
            set { _loggingDaysAtTd = value; }
        }

        public double? UnderReamingDays
        {
            get { return _underReamingDays; }
            set { _underReamingDays = value; }
        }

        public double? UnderReamedInterval
        {
            get { return _underReamedInterval; }
            set { _underReamedInterval = value; }
        }

        public string Fewd
        {
            get { return _fewd; }
            set { _fewd = value; }
        }

        public string AgeOfDeepestResevoir
        {
            get { return _ageOfDeepestResevoir; }
            set { _ageOfDeepestResevoir = value; }
        }

        public string NewTechniques
        {
            get { return _newTechniques; }
            set { _newTechniques = value; }
        }

        public double? RigMoveTime
        {
            get { return _rigMoveTime; }
            set { _rigMoveTime = value; }
        }

        public string RigMoveWithinField
        {
            get { return _rigMoveWithinField; }
            set { _rigMoveWithinField = value; }
        }

        public double? GeoSideTrackWhipstockDays
        {
            get { return _geoSideTrackWhipstockDays; }
            set { _geoSideTrackWhipstockDays = value; }
        }

        public double? SlotRecoveryDays
        {
            get { return _slotRecoveryDays; }
            set { _slotRecoveryDays = value; }
        }

        public string OfflineSlotRecoveryOps
        {
            get { return _offlineSlotRecoveryOps; }
            set { _offlineSlotRecoveryOps = value; }
        }

        public string BatchSetDrilled
        {
            get { return _batchSetDrilled; }
            set { _batchSetDrilled = value; }
        }

        public DateTime? SpudDate
        {
            get { return _spudDate; }
            set { _spudDate = value; }
        }

        public double? DryHoleDays
        {
            get { return _dryHoleDays; }
            set { _dryHoleDays = value; }
        }

        public DateTime? EndOfDryholePeriod
        {
            get { return _endOfDryholePeriod; }
            set { _endOfDryholePeriod = value; }
        }

        public int? DryHoleSuspensions
        {
            get { return _dryHoleSuspensions; }
            set { _dryHoleSuspensions = value; }
        }

        public double? DaysSpentSuspensions
        {
            get { return _daysSpentSuspensions; }
            set { _daysSpentSuspensions = value; }
        }

        public double? TotalWellSiteDays
        {
            get { return _totalWellSiteDays; }
            set { _totalWellSiteDays = value; }
        }

        public double? DryHoleCost
        {
            get { return _dryHoleCost; }
            set { _dryHoleCost = value; }
        }

        public string PrelimOrFinalCost
        {
            get { return _prelimOrFinalCost; }
            set { _prelimOrFinalCost = value; }
        }

        public string CompletenessOfCost
        {
            get { return _completenessOfCost; }
            set { _completenessOfCost = value; }
        }

        public double? TotalWellCost
        {
            get { return _totalWellCost; }
            set { _totalWellCost = value; }
        }

        public string Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        public string WellStauts
        {
            get { return _wellStauts; }
            set { _wellStauts = value; }
        }

        public double? PaSuCoDays
        {
            get { return _paSuCoDays; }
            set { _paSuCoDays = value; }
        }

        public double? OtherOPdays
        {
            get { return _otherOPdays; }
            set { _otherOPdays = value; }
        }

        public DateTime? EndOfWellOps
        {
            get { return _endOfWellOps; }
            set { _endOfWellOps = value; }
        }

        public double? WowBeforeMoveOff
        {
            get { return _wowBeforeMoveOff; }
            set { _wowBeforeMoveOff = value; }
        }

        public string RigMooringSystem
        {
            get { return _rigMooringSystem; }
            set { _rigMooringSystem = value; }
        }

        public double? DaysSpentOnMooring
        {
            get { return _daysSpentOnMooring; }
            set { _daysSpentOnMooring = value; }
        }

        public double? DaysSpentDeMooring
        {
            get { return _daysSpentDeMooring; }
            set { _daysSpentDeMooring = value; }
        }

        public double? WowDuringMooring
        {
            get { return _wowDuringMooring; }
            set { _wowDuringMooring = value; }
        }

        public double? WowDuringDeMooring
        {
            get { return _wowDuringDeMooring; }
            set { _wowDuringDeMooring = value; }
        }

        public double? TotalWowDuringDryhole
        {
            get { return _totalWowDuringDryhole; }
            set { _totalWowDuringDryhole = value; }
        }

        public double? TotalNptExclWow
        {
            get { return _totalNptExclWow; }
            set { _totalNptExclWow = value; }
        }

        public double? NptDueToContractor
        {
            get { return _nptDueToContractor; }
            set { _nptDueToContractor = value; }
        }

        public double? NptDueToServiceCompany
        {
            get { return _nptDueToServiceCompany; }
            set { _nptDueToServiceCompany = value; }
        }

        public double? NptDueToOperatorProbs
        {
            get { return _nptDueToOperatorProbs; }
            set { _nptDueToOperatorProbs = value; }
        }

        public double? NptDueToExternalProbs
        {
            get { return _nptDueToExternalProbs; }
            set { _nptDueToExternalProbs = value; }
        }

        public double? NptDueToDownholeProbs
        {
            get { return _nptDueToDownholeProbs; }
            set { _nptDueToDownholeProbs = value; }
        }

        public string MainInterruptsDryHole
        {
            get { return _mainInterruptsDryHole; }
            set { _mainInterruptsDryHole = value; }
        }

        public string FurtherDetails
        {
            get { return _furtherDetails; }
            set { _furtherDetails = value; }
        }

        public string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        public string Comments2
        {
            get { return _comments2; }
            set { _comments2 = value; }
        }

        public string ApiWellNumber
        {
            get { return _apiWellNumber; }
            set { _apiWellNumber = value; }
        }

        public string LwlWellName
        {
            get { return _lwlWellName; }
            set { _lwlWellName = value; }
        }

        public double? LwlLoggingTime
        {
            get { return _lwlLoggingTime; }
            set { _lwlLoggingTime = value; }
        }

        public string DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }

        public List<ValidationError> Errors
        {
            get { return _errors; }
            set { _errors = value; }
        }

        public int Row
        {
            get { return _row; }
            set { _row = value; }
        }

#endregion



    }
}
